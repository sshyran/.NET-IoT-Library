﻿using System;
using System.Threading;
using Iot.Device.Arduino;

namespace ArduinoCsCompiler.Runtime
{
    [ArduinoReplacement(typeof(System.Environment), true)]
    internal static class MiniEnvironment
    {
        public static int CurrentManagedThreadId => Thread.CurrentThread.ManagedThreadId;

        public static int TickCount
        {
            [ArduinoImplementation("EnvironmentTickCount")]
            get
            {
                throw new NotImplementedException();
            }
        }

        public static int TickCount64
        {
            [ArduinoImplementation("EnvironmentTickCount64")]
            get
            {
                throw new NotImplementedException();
            }
        }

        public static int ProcessorCount
        {
            [ArduinoImplementation("EnvironmentProcessorCount")]
            get
            {
                return 1;
            }
        }

        public static int ProcessId
        {
            get
            {
                // Some magic number
                return 0x1BBAEFFE;
            }
        }

        public static bool IsSingleProcessor
        {
            get
            {
                return ProcessorCount == 1;
            }
        }

        public static string SystemDirectory
        {
            get
            {
                return "/"; // At the moment, we do not have a file system at all
            }
        }

        public static string NewLine
        {
            get
            {
                return "\n"; // We'll have our "Arduino-OS" look like an unix style system
            }
        }

        internal static System.Boolean IsWindows8OrAbove
        {
            get
            {
                return true;
            }
        }

        public static OperatingSystem OSVersion
        {
            get
            {
                // This does not have a "anything else" option...
                return new OperatingSystem(PlatformID.Unix, new Version(1, 0));
            }
        }

        [ArduinoImplementation("EnvironmentFailFast1")]
        public static void FailFast(string message)
        {
            throw new NotImplementedException();
        }

        [ArduinoImplementation("EnvironmentFailFast2")]
        public static void FailFast(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public static string? GetEnvironmentVariable(string variable)
        {
            return null;
        }

        public static string ExpandEnvironmentVariables(string input)
        {
            return input;
        }
    }
}
