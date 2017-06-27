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

			string embeddedPlayerPath = Path.GetTempPath () + "/Unity3DPlugin/Embedded/"
			                            + System.Diagnostics.Process.GetCurrentProcess().Id;
			Directory.CreateDirectory (embeddedPlayerPath);

			string portFile = embeddedPlayerPath + "/port" + port;
			using (File.Create (portFile)) {
			}
			;

            //Debug
            //StartCoroutine(SimulateTrigger());
		}

		public void Update ()
		{
			if (listener.Pending ()) {
				Socket soc = listener.AcceptSocket ();

				Destroy (meshNode);

				string importDir = Loader.GetImportDir (soc);
				Debug.Log ("Importing from:" + importDir);
				soc.Disconnect (false);

				meshNode = Loader.ImportGameObject(importDir);
                meshNode.transform.position = new Vector3(0, 1, 0);

                // Register object in globals
                if(meshNode != null)
                    GlobalVariables.RegisterParaviewObject(meshNode);

                // Automaticall Resize object
                meshNode.AddComponent<AutoResize>();

				meshNode.SetActive (true);
			}
		}

		void OnApplicationQuit ()
		{
            if(listener != null)
			    listener.Stop ();
			listener = null;
		}

        private IEnumerator SimulateTrigger()
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("Trigger load");
            GlobalVariables.RegisterParaviewObject(new GameObject("DUMMY"));
        }
	}
}