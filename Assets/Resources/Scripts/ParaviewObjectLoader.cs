namespace ParaUnity
{
    using UnityEngine;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class ParaviewObjectLoader : MonoBehaviour
    {
        private GameObject meshNode;
        private TcpListener listener;

        private bool isImporting = false;

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

        void Update()
        {
            if (listener.Pending())
            {
                // Received incoming connection from Paraview
                Socket socket = listener.AcceptSocket();

                // Destroy previous object if I'm not still waiting for frames
                if (!isImporting)
                {
                    if (Globals.paraviewObj != null)
                        Globals.UnregisterParaviewObject();

                    Destroy(meshNode);
                }

                // Get the message from the socket and split it over ";;"
                string message = GetMessage(socket);
                string[] args = message.Split(new[] { ";;" }, System.StringSplitOptions.None);

                // If is a well-formatted message
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

                    // If I just loaded the last frame
                    if (currentFrame == totalFrames - 1)
                    {
                        meshNode = Loader.MergeFramesIntoGameObject();
                        isImporting = false;
                    }

                    // If I am not waiting for other frames or object has no animation
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

            // Create directory if it doesn't exist
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
                using (File.Create(portFile)) { };
            }
        }

        private string GetMessage(Socket soc)
        {
            byte[] b = new byte[2048];
            StringBuilder sb = new StringBuilder();

            int k = soc.Receive(b);
            for (int i = 0; i < k; i++)
            {
                sb.Append(System.Convert.ToChar(b[i]));
            }

            string str = sb.ToString();

            //Globals.logger.Log("Received message on socket: " + str);

            return str;
        }

        private void SendMessage(Socket soc, string message)
        {
            soc.Send(Encoding.ASCII.GetBytes(message));
        }

        private void SendAckForFrame(Socket soc, int frame)
        {
            SendMessage(soc, "OK " + frame);
        }

        private void SendAck(Socket soc)
        {
            SendMessage(soc, "OK");
        }

        void OnApplicationQuit()
        {
            if (listener != null)
                listener.Stop();
            listener = null;
        }
    }
}