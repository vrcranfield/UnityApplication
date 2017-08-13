using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Xml.Linq;
using UnityEngine;

/**
 * Class for loading X3D data from RAM
 */
public class XDocumentLoader
{
    // Import OpenFileMapping from Windows SDK
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern SafeFileHandle OpenFileMapping(   uint dwDesiredAccess,
                                                    bool bInheritHandle,
                                                    string lpName);

    // Import MapViewOfFile from Windows SDK
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr MapViewOfFile( SafeFileHandle hFileMappingObject,
                                        UInt32 dwDesiredAccess,
                                        UInt32 dwFileOffsetHigh,
                                        UInt32 dwFileOffsetLow,
                                        UIntPtr dwNumberOfBytesToMap);

    // Constants
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

    // Fields
    static private SafeFileHandle sHandle;
    static private IntPtr hHandle;
    static private IntPtr pBuffer;

    /**
     * Reads an X3D object in named shared memory and converts it to XDocument.
     */
    public static XDocument Load(string objectName, uint objectSize)
    {
        sHandle = new SafeFileHandle(hHandle, true);

        // Attach to object in memory
        bool attachSuccessful = Attach(objectName, objectSize);

        if (!attachSuccessful)
            return null;

        // Parse data into XDocument
        XDocument doc = XDocument.Parse(Marshal.PtrToStringAnsi(pBuffer, Convert.ToInt32(objectSize)));

        // Detach file mapping
        Detach();

        // Return document
        return doc;
    }

    /**
     * Creates file mapping to an object in shared memory and maps it in process memory
     */
    private static bool Attach(string objectName, UInt32 objectSize)
    {
        if (!sHandle.IsInvalid)
        {
            Debug.LogWarning("Attach called on preexisting shared memory handle");
            return false;
        }

        // Open file mapping
        sHandle = OpenFileMapping(FILE_MAP_ALL_ACCESS, false, objectName);

        if (sHandle.IsInvalid)
        {
            Debug.LogWarning("Failed to open shared memory");
            return false;
        }

        // Map shared memory space in process space
        pBuffer = MapViewOfFile(sHandle, FILE_MAP_ALL_ACCESS, 0, 0, new UIntPtr(objectSize));

        if (pBuffer.Equals(UIntPtr.Zero))
        {
            Debug.LogWarning("Failed to map from shared memory");
        }

        return true;
    }

    /**
     * Detaches from main memory
     */
    private static void Detach()
    {
        if (!sHandle.IsInvalid && !sHandle.IsClosed)
            sHandle.Close();
    
        pBuffer = IntPtr.Zero;
    }
}
