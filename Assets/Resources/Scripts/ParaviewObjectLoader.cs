namespace ParaUnity
{
    using UnityEngine;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    public class ParaviewObjectLoader : MonoBehaviour
    {
        private GameObject meshNode;
        private TcpListener listener;

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
                Globals.logger.Log("Received incoming connection");
                Socket soc = listener.AcceptSocket();

                if(Globals.paraviewObj != null)
                {
                    Globals.UnregisterParaviewObject();
                }
                Destroy(meshNode);

                string importDir = Loader.GetImportDir(soc);

                if (Directory.Exists(importDir) || File.Exists(importDir))
                {
                    Globals.logger.Log("Importing from: " + importDir);

                    meshNode = Loader.ImportGameObject(importDir);
                    Globals.logger.Log("Finished importing");

                    meshNode.name = "ParaviewObject";
                    meshNode.transform.position = new Vector3(0, 1, 0);
                    meshNode.AddComponent<Interactable>();

                    Globals.RegisterParaviewObject(meshNode);

                    meshNode.SetActive(true);
                }
                else if(importDir.Equals("TEST"))
                {
                    Globals.logger.Log("Receiving object from Paraview...");
                } else
                {
                    Globals.logger.LogWarning("Message was not existing file/directory: " + importDir);
                }

                soc.Disconnect(false);
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