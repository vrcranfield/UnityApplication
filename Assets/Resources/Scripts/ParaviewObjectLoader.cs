namespace ParaUnity
{
    using UnityEngine;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using ParaUnity.X3D;
    using System.Text;

    public class ParaviewObjectLoader : MonoBehaviour
    {
        private GameObject meshNode;
        private TcpListener listener;
        private bool isLoadingAnimatedObject = false;

        public void Start()
        {
            ModeManager modeManager = Globals.modeManager;

            listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;

            Globals.logger.Log("Started listening on port: " + port);

            string dir = Path.GetTempPath() + "/Unity3DPlugin/";

            if(modeManager.isEditorMode())
                dir += "Editor/" + port;
            else
                dir += "/Embedded/" + System.Diagnostics.Process.GetCurrentProcess().Id;

            Directory.CreateDirectory(dir);

            if (!modeManager.isEditorMode()) {
                string portFile = dir + "/port" + port;
                using (File.Create(portFile)){};
            }
        }

        public void Update()
        {
            if (listener.Pending())
            {
                // Globals.logger.Log("Received incoming connection");
                Socket socket = listener.AcceptSocket();

                // Destroy previous object if I'm not still receiving frames
                if (!isLoadingAnimatedObject)
                {
                    if (Globals.paraviewObj != null)
                        Globals.UnregisterParaviewObject();

                    Destroy(meshNode);
                }

                string message = Loader.GetMessage(socket);

                string[] args = message.Split(new[] { ";;" }, System.StringSplitOptions.None);

                // If is a well-formatted message
                if (args.Length > 1)
                {
                    string objectName = args[0];
                    uint objectSize = System.Convert.ToUInt32(args[1], 10);
                     
                    // Globals.logger.Log("Importing object");

                    // Multiple frames
                    if(args.Length == 4)
                    {
                        isLoadingAnimatedObject = true;

                        int currentFrame = System.Convert.ToInt32(args[2]);
                        int totalFrames = System.Convert.ToInt32(args[3]);

                        Loader.ImportFrame(objectName, objectSize);
                        socket.Send(Encoding.ASCII.GetBytes("OK " + currentFrame.ToString()));

                        if (currentFrame == totalFrames - 1)
                        {
                            meshNode = Loader.MergeFramesIntoGameObject();
                            isLoadingAnimatedObject = false;
                        }
                    }

                    else
                    {
                        meshNode = Loader.ImportSimpleGameObject(objectName, objectSize);
                        isLoadingAnimatedObject = false;
                    }

                    if(!isLoadingAnimatedObject)
                    {
                        // Globals.logger.Log("Finished importing");

                        // Send reply to Paraview for memory cleanup
                        socket.Send(Encoding.ASCII.GetBytes("OK"));

                        meshNode.name = "ParaviewObject";
                        meshNode.transform.position = new Vector3(0, 1, 0);

                        if (meshNode.GetComponentInChildren<MeshRenderer>() != null)
                        {
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
                else if(!message.Equals(""))
                {
                    Globals.logger.LogWarning("Unrecognized message format: " + message);
                }

            }
        }

        void OnApplicationQuit()
        {
            if (listener != null)
                listener.Stop();
            listener = null;
        }
    }
}