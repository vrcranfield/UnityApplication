﻿namespace ParaUnity.X3D
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32.SafeHandles;
    using System.Xml.Linq;
    using UnityEngine;
    using System.IO;
    using System.Text;

    public class XDocumentLoader
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle OpenFileMapping(   uint dwDesiredAccess,
                                                        bool bInheritHandle,
                                                        string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile( SafeFileHandle hFileMappingObject,
                                            UInt32 dwDesiredAccess,
                                            UInt32 dwFileOffsetHigh,
                                            UInt32 dwFileOffsetLow,
                                            UIntPtr dwNumberOfBytesToMap);

        const UInt32 STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        const UInt32 SECTION_QUERY = 0x0001;
        const UInt32 SECTION_MAP_WRITE = 0x0002;
        const UInt32 SECTION_MAP_READ = 0x0004;
        const UInt32 SECTION_MAP_EXECUTE = 0x0008;
        const UInt32 SECTION_EXTEND_SIZE = 0x0010;
        const UInt32 SECTION_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SECTION_QUERY |
            SECTION_MAP_WRITE |
            SECTION_MAP_READ |
            SECTION_MAP_EXECUTE |
            SECTION_EXTEND_SIZE);
        const UInt32 FILE_MAP_ALL_ACCESS = SECTION_ALL_ACCESS;

        static private SafeFileHandle sHandle;
        static private IntPtr hHandle;
        static private IntPtr pBuffer;

        public static XDocument Load(string objectName, uint objectSize)
        {
            sHandle = new SafeFileHandle(hHandle, true);
            bool attachSuccessful = Attach(objectName, objectSize);

            if (attachSuccessful)
            {
                //Debug.Log("Parsing document from shared memory");
                XDocument doc = XDocument.Parse(Marshal.PtrToStringAnsi(pBuffer, Convert.ToInt32(objectSize)));
                //Debug.Log("Finished parsing document from shared memory");

                Detach();
                return doc;
            }

            return null;
        }

        private static bool Attach(string SharedMemoryName, UInt32 NumBytes)
        {
            if (!sHandle.IsInvalid)
            {
                Debug.LogWarning("Attach called on preexisting shared memory handle");
                return false;
            }

            sHandle = OpenFileMapping(FILE_MAP_ALL_ACCESS, false, SharedMemoryName);

            if (sHandle.IsInvalid)
            {
                Debug.LogWarning("Failed to open shared memory");
                return false;
            }

            pBuffer = MapViewOfFile(sHandle, FILE_MAP_ALL_ACCESS, 0, 0, new UIntPtr(NumBytes));

            if (pBuffer.Equals(UIntPtr.Zero))
            {
                Debug.LogWarning("Failed to map from shared memory");
            }

            return true;
        }

        private static void Detach()
        {
            if (!sHandle.IsInvalid && !sHandle.IsClosed)
            {
                //fair to leak if can't close
                sHandle.Close();
            }
            pBuffer = IntPtr.Zero;
        }
    }
}
