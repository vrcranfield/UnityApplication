using UnityEngine;
using ParaUnity;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

/**
 * Class responsible to communicate with ParaUnity and handle object importing
 */
public class ParaviewObjectLoader : MonoBehaviour
{
    // Fields
    private GameObject meshNode;
    private TcpListener listener;
    private bool isImporting = false;

    /**
     * Called at object's initialization, after all Awakes.
     */
    void Start()
    {
        // Set up welcoming socket
        listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;

        Globals.logger.Log("Started listening on port: " + port);

        // Create the port file
        createPortDir(port);
    }

    /**
     * Called at every frame
     */
    void Update()
    {
        if (listener.Pending())
        {
            // Received incoming connection from Paraview
            Socket socket = listener.AcceptSocket();

            // Destroy previous object if not waiting for frames
            if (!isImporting)
            {
                if (Globals.paraviewObj != null)
                    Globals.UnregisterParaviewObject();

                Destroy(meshNode);
            }

            // Get the message from the socket and split it over ";;"
            string message = GetMessage(socket);
            string[] args = message.Split(new[] { ";;" }, System.StringSplitOptions.None);

            // If it's a well-formatted message
            if (args.Length > 1)
            {
                string objectName = args[0];
                uint objectSize = System.Convert.ToUInt32(args[1], 10); //in bytes
                int currentFrame = 0;
                int totalFrames = 1;

                // If there are multiple frames
                if (args.Length == 4)
                {
                    isImporting = true;

                    currentFrame = System.Convert.ToInt32(args[2]);
                    totalFrames = System.Convert.ToInt32(args[3]);
                }

                Loader.ImportFrame(objectName, objectSize);

                int percentage = System.Convert.ToInt32(100 * (float)(currentFrame + 1) / totalFrames);
                Globals.logger.Log("Importing object: " + percentage + "%");

                if(totalFrames > 1)
                    SendAckForFrame(socket, currentFrame);

                // If it just loaded the last frame
                if (currentFrame == totalFrames - 1)
                {
                    meshNode = Loader.MergeFramesIntoGameObject();
                    isImporting = false;
                }

                // If it isn't waiting for other frames or object has no animation
                if(!isImporting)
                {
                    // Send acknowledgment to Paraview for memory cleanup
                    SendAck(socket);

                    // Set up the unity object
                    meshNode.name = "ParaviewObject";
                    meshNode.transform.position = new Vector3(0, 1, 0);

                    // If the object is not an empty object
                    if (meshNode.GetComponentInChildren<MeshRenderer>() != null)
                    {
                        // Set it up and notify the listeners
                        meshNode.AddComponent<Interactable>();
                        Globals.RegisterParaviewObject(meshNode);
                        meshNode.SetActive(true);
                        Globals.logger.Log("Object correctly imported from ParaView");
                    }
                    else
                    {
                        Globals.logger.LogWarning("The object sent from Paraview was empty");
                    }
                }
            }
            // If the message had a bad format and is not empty
            else if(!message.Equals(""))
            {
                Globals.logger.LogWarning("Unrecognized message format: " + message);
            }
        }
    }


    /**
     * Creates directory with port file
     */
    private void createPortDir(int port)
    {
        ModeManager modeManager = Globals.modeManager;

        string dir = Path.GetTempPath() + "/Unity3DPlugin/";

        // Create directory if it doesn't exist
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        if (modeManager.isEditorMode())
            dir += "Editor/";
        else
            dir += "Embedded/";

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // Cleanup leftovers from previous runs
        foreach(string d in Directory.GetDirectories(dir))
        {
            Directory.Delete(d, true);
        }

        if (modeManager.isEditorMode())
            dir += port;
        else
            dir += System.Diagnostics.Process.GetCurrentProcess().Id;

        Directory.CreateDirectory(dir);

        if (!modeManager.isEditorMode())
        {
            string portFile = dir + "/port" + port;

            // Create port file
            using (File.Create(portFile)) { };
        }
    }

    /**
     * Read messange from incoming connection and convert to string
     */
    private string GetMessage(Socket soc)
    {
        byte[] b = new byte[2048];
        StringBuilder sb = new StringBuilder();

        int k = soc.Receive(b);
        for (int i = 0; i < k; i++)
        {
            sb.Append(System.Convert.ToChar(b[i]));
        }
        
        return sb.ToString();
    }

    /**
     * Sends message over socket
     */
    private void SendMessage(Socket soc, string message)
    {
        soc.Send(Encoding.ASCII.GetBytes(message));
    }

    /**
     * Send acknowledgement for imported frame
     */
    private void SendAckForFrame(Socket soc, int frame)
    {
        SendMessage(soc, "OK " + frame);
    }

    /**
     * Send general acknowledgement
     */
    private void SendAck(Socket soc)
    {
        SendMessage(soc, "OK");
    }

    /**
     * Stop TCP Socket before quitting the application
     */
    void OnApplicationQuit()
    {
        if (listener != null)
            listener.Stop();
        listener = null;
    }
}