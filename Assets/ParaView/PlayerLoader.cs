namespace ParaUnity
{
    using UnityEngine;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Collections;

    public class PlayerLoader : MonoBehaviour
	{
		private GameObject meshNode;
		private TcpListener listener;

		public void Start ()
		{
			listener = new TcpListener (IPAddress.Loopback, 0);
			listener.Start ();
			int port = ((IPEndPoint)listener.LocalEndpoint).Port;
			Debug.Log ("Started listening on port: " + port);

			string dir = Path.GetTempPath () + "/Unity3DPlugin/Embedded/"
			                            + System.Diagnostics.Process.GetCurrentProcess().Id;
			Directory.CreateDirectory (dir);

			string portFile = dir + "/port" + port;
			using (File.Create (portFile)) {
			}
			;

		}

		public void Update ()
		{
			if (listener.Pending ()) {
				Socket soc = listener.AcceptSocket ();

				Destroy (meshNode);

				string importDir = Loader.GetImportDir (soc);

                if (Directory.Exists(importDir) || File.Exists(importDir))
                {
                    Debug.Log("Importing from:" + importDir);

                    meshNode = Loader.ImportGameObject(importDir);
                    meshNode.transform.position = new Vector3(0, 1, 0);

                    // Register object in globals
                    if (meshNode != null)
                        GlobalVariables.RegisterParaviewObject(meshNode);

                    // Automaticall Resize object
                    meshNode.AddComponent<AutoResize>();

                    meshNode.SetActive(true);
                }

                soc.Disconnect(false);
            }
		}

		void OnApplicationQuit ()
		{
            if(listener != null)
			    listener.Stop ();
			listener = null;
		}
	}
}