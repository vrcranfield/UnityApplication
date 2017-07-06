﻿namespace ParaUnity.X3D
{
	using System;
	using System.Globalization;
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;
    using UnityEngine;

	public class X3DLoader
	{
        private X3DSceneHandler sceneHandler = new X3DSceneHandler();

		public X3DScene Load(string x3dFile)
		{
			XDocument doc = XDocument.Load (x3dFile);
            return Load(doc);
		}

        public X3DScene Load(string name, uint size)
        {
            XDocument doc = XDocumentLoader.Load(name, size);
            return Load(doc);
        }

        public X3DScene Load(XDocument doc)
        {
            return (X3DScene)sceneHandler.Parse(doc.Element("X3D").Element("Scene"));
        }
    }
}