﻿using System;
using System.Runtime.InteropServices;

namespace Iot.Device.Arduino.Runtime
{
    internal partial class MiniInterop
    {
        [ArduinoReplacement("Interop+Kernel32", "System.Private.CoreLib.dll", true, IncludingSubclasses = true, IncludingPrivates = true)]
        internal static class Kernel32
        {
            internal const uint LOCALE_ALLOW_NEUTRAL_NAMES = 0x08000000; // Flag to allow returning neutral names/lcids for name conversion
            internal const uint LOCALE_ILANGUAGE = 0x00000001;
            internal const uint LOCALE_SUPPLEMENTAL = 0x00000002;
            internal const uint LOCALE_REPLACEMENT = 0x00000008;
            internal const uint LOCALE_NEUTRALDATA = 0x00000010;
            internal const uint LOCALE_SPECIFICDATA = 0x00000020;
            internal const uint LOCALE_SISO3166CTRYNAME = 0x0000005A;
            internal const uint LOCALE_SNAME = 0x0000005C;
            internal const uint LOCALE_INEUTRAL = 0x00000071;
            internal const uint LOCALE_SSHORTTIME = 0x00000079;
            internal const uint LOCALE_ICONSTRUCTEDLOCALE = 0x0000007d;
            internal const uint LOCALE_STIMEFORMAT = 0x00001003;
            internal const uint LOCALE_IFIRSTDAYOFWEEK = 0x0000100C;
            internal const uint LOCALE_RETURN_NUMBER = 0x20000000;
            internal const uint LOCALE_NOUSEROVERRIDE = 0x80000000;

            public static unsafe uint GetFullPathNameW(ref Char lpFileName, UInt32 nBufferLength, ref Char lpBuffer, IntPtr lpFilePart)
            {
                throw new NotImplementedException();
            }

            private static unsafe int AssignCharData(char* value, int valueLength, string data)
            {
                if (valueLength < data.Length + 1)
                {
                    return data.Length + 1;
                }

                int i = 0;
                for (i = 0; i < data.Length; i++)
                {
                    value[i] = data[i];
                }

                value[i] = '\0';

                return data.Length + 1;
            }

            private static unsafe int AssignNumber(char* value, int valueLength, ushort number)
            {
                if (valueLength < 2)
                {
                    return 2;
                }

                // This actually returns a DWORD in a place where a string would normally be. Don't ask me who designed an interface this way
                ushort* ptr = (ushort*)value;
                *ptr = number;
                return 2;
            }

            public static unsafe int GetLocaleInfoEx(string lpLocaleName, uint lcType, void* lpLCData, int cchData)
            {
                return GetLocaleInfoEx(lpLocaleName, lcType, (char*)lpLCData, cchData);
            }

            public static unsafe int GetLocaleInfoEx(string lpLocaleName, uint lcType, char* lpLCData, int cchData)
            {
                uint typeToQuery = lcType & 0xFFFF; // Ignore high-order bits
                bool returnNumber = (lcType & LOCALE_RETURN_NUMBER) != 0;
                switch (typeToQuery)
                {
                    case LOCALE_ICONSTRUCTEDLOCALE:
                        if (returnNumber)
                        {
                            return AssignNumber(lpLCData, cchData, 0);
                        }

                        return AssignCharData(lpLCData, cchData, "0");
                    case LOCALE_ILANGUAGE:
                    case LOCALE_IFIRSTDAYOFWEEK:
                        if (returnNumber)
                        {
                            return AssignNumber(lpLCData, cchData, 0);
                        }

                        return AssignCharData(lpLCData, cchData, "0");
                    case LOCALE_INEUTRAL:
                        if (returnNumber)
                        {
                            return AssignNumber(lpLCData, cchData, 1);
                        }

                        return AssignCharData(lpLCData, cchData, "1");
                    case LOCALE_SNAME:
                        return AssignCharData(lpLCData, cchData, "World");
                    case LOCALE_SISO3166CTRYNAME:
                        return AssignCharData(lpLCData, cchData, "WRL");

                    default:
                        throw new NotSupportedException();
                }
            }

            internal static unsafe int CompareStringEx(
                char* lpLocaleName,
                uint dwCmpFlags,
                char* lpString1,
                int cchCount1,
                char* lpString2,
                int cchCount2,
                void* lpVersionInformation,
                void* lpReserved,
                IntPtr lParam)
            {
                throw new NotImplementedException();
            }

            internal static unsafe int CompareStringOrdinal(
                char* lpString1,
                int cchCount1,
                char* lpString2,
                int cchCount2,
                bool bIgnoreCase)
            {
                throw new NotImplementedException();
            }

            internal static unsafe int LCMapStringEx(string lpLocaleName, uint dwMapFlags, char* lpSrcStr, int cchsrc, void* lpDestStr,
                int cchDest, void* lpVersionInformation, void* lpReserved, IntPtr sortHandle)
            {
                throw new NotImplementedException();
            }

            internal static int FindNLSString(
                int locale,
                uint flags,
                [MarshalAs(UnmanagedType.LPWStr)] string sourceString,
                int sourceCount,
                [MarshalAs(UnmanagedType.LPWStr)] string findString,
                int findCount,
                out int found)
            {
                // NLS is not active, we should never really get here.
                throw new NotImplementedException();
            }

            internal static unsafe int FindNLSStringEx(
                char* lpLocaleName,
                uint dwFindNLSStringFlags,
                char* lpStringSource,
                int cchSource,
                char* lpStringValue,
                int cchValue,
                int* pcchFound,
                void* lpVersionInformation,
                void* lpReserved,
                IntPtr sortHandle)
            {
                // NLS is not active, we should never really get here.
                throw new NotImplementedException();
            }

            [ArduinoImplementation(NativeMethod.InteropQueryPerformanceFrequency)]
            internal static unsafe bool QueryPerformanceFrequency(long* lpFrequency)
            {
                return true;
            }

            [ArduinoImplementation(NativeMethod.InteropQueryPerformanceCounter)]
            internal static unsafe bool QueryPerformanceCounter(long* lpCounter)
            {
                return true;
            }

            internal static unsafe uint GetTempPathW(int bufferLen, ref char buffer)
            {
                // We require 5 chars, including the terminating 0
                if (bufferLen < 5)
                {
                    return 5;
                }

                char* ptr = (char*)MiniUnsafe.AsPointer(ref buffer);
                ptr[0] = '/';
                ptr[1] = 't';
                ptr[2] = 'm';
                ptr[3] = 'p';
                ptr[4] = '\0';
                return 4; // the return value is the number of chars copied, not including the 0
            }

            [ArduinoImplementation(NativeMethod.Interop_Kernel32CreateFile, CompareByParameterNames = true)]
            internal static unsafe Microsoft.Win32.SafeHandles.SafeFileHandle CreateFile(System.String lpFileName, System.Int32 dwDesiredAccess,
                System.IO.FileShare dwShareMode, SECURITY_ATTRIBUTES* lpSecurityAttributes, System.IO.FileMode dwCreationDisposition, System.Int32 dwFlagsAndAttributes, System.IntPtr hTemplateFile)
            {
                throw new NotImplementedException();
            }

            [ArduinoImplementation(NativeMethod.Interop_Kernel32SetFilePointerEx)]
            internal static System.Boolean SetFilePointerEx(Microsoft.Win32.SafeHandles.SafeFileHandle hFile, System.Int64 liDistanceToMove, ref System.Int64 lpNewFilePointer, System.UInt32 dwMoveMethod)
            {
                throw new NotImplementedException();
            }

            internal static System.Boolean SetThreadErrorMode(System.UInt32 dwNewMode, ref System.UInt32 lpOldMode)
            {
                return true;
            }

            [ArduinoImplementation(NativeMethod.Interop_Kernel32CloseHandle)]
            internal static System.Boolean CloseHandle(System.IntPtr handle)
            {
                throw new NotImplementedException();
            }

            internal static string GetMessage(int errorCode)
            {
                // Couldn't get a message, so manufacture one.
                return string.Format("OS error (0x{0:x})", errorCode);
            }

            [ArduinoImplementation(NativeMethod.Interop_Kernel32SetLastError)]
            internal static void SetLastError(uint errorCode)
            {
                throw new NotImplementedException();
            }

            [ArduinoImplementation(NativeMethod.Interop_Kernel32GetLastError)]
            internal static uint GetLastError()
            {
                throw new NotImplementedException();
            }

            internal static int GetFileType(SafeHandle hFile)
            {
                SetLastError(0);
                return 1; // Only disk files supported
            }

            internal static Boolean SetEvent(Microsoft.Win32.SafeHandles.SafeWaitHandle handle)
            {
                return true;
            }
        }

        internal struct SECURITY_ATTRIBUTES
        {
            public int DummyData;
        }

        [ArduinoReplacement("Interop+Kernel32", "System.IO.FileSystem.dll", true, typeof(System.IO.File), IncludingPrivates = true)]
        internal static class Kernel32FileSystem
        {
            [ArduinoImplementation]
            internal static string GetMessage(Int32 errorCode)
            {
                return GetMessage(errorCode, IntPtr.Zero);
            }

            [ArduinoImplementation]
            internal static string GetMessage(int errorCode, IntPtr moduleHandle)
            {
                // Couldn't get a message, so manufacture one.
                return string.Format("OS error (0x{0:x})", errorCode);
            }

            [ArduinoImplementation(CompareByParameterNames = true)]
            internal static bool CreateDirectory(string path, ref SECURITY_ATTRIBUTES lpSecurityAttributes)
            {
                throw new NotImplementedException();
            }
        }
    }
}
