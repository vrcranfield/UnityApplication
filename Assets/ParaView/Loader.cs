namespace ParaUnity
{
	using ParaUnity.X3D;
	using UnityEngine;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Text;
	using System.Text.RegularExpressions;


    public class Loader
	{
		private static X3DLoader LOADER = new X3DLoader ();
        private static List<GameObject> frames = new List<GameObject>();
        
        public static void ImportFrame(string name, uint size)
        {
            frames.Add((GameObject)LOADER.Load(name, size));
        }

        public static GameObject MergeFramesIntoGameObject()
        {
            MergeFrames(frames);
            for (int i = 1; i < frames.Count; i++)
            {
                GameObject.Destroy(frames[i]);
            }
            GameObject obj = frames[0];
            frames.Clear();
            return obj;
        }

        private static GameObject MergeFrames(List<GameObject> frames) {
			if (frames [0].transform.childCount > 0 &&
				frames [0].transform.GetChild(0).GetComponent<Renderer> () != null) {
				GameObject frameContainer = new GameObject ("FramedObject");
				frameContainer.transform.parent = frames [0].transform.parent;
				frames [0].transform.parent = frameContainer.transform;
				for (int i = 1; i < frames.Count; i++) {
					frames[i].transform.parent = frameContainer.transform;
					frames [i].SetActive (false);
				}
			} else {
				for (int i = 0; i < frames [0].transform.childCount; i++) {
					MergeFrames (frames.Select (obj => obj.transform.GetChild (i).gameObject).ToList ());
				}
			}
			return frames [0];
		}
    }
}