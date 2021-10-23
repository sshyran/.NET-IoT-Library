﻿using System;
using Iot.Device.Arduino;

namespace ArduinoCsCompiler.Runtime
{
    [ArduinoReplacement("System.IO.FileSystem", "System.IO.FileSystem.dll", true, typeof(System.IO.File), IncludingPrivates = true)]
    internal static class MiniFileSystem
    {
        [ArduinoImplementation("FileSystemCreateDirectory")]
        public static void CreateDirectory(string fullPath, byte[] securityDescriptor)
        {
            throw new NotImplementedException();
        }

        [ArduinoImplementation("FileSystemFileExists")]
        public static bool FileExists(string fullPath)
        {
            throw new NotImplementedException();
        }
    }
}
