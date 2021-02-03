﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iot.Device.Arduino
{
    [ArduinoReplacement(typeof(System.Runtime.CompilerServices.RuntimeHelpers), true)]
    internal static class MiniRuntimeHelpers
    {
        [ArduinoImplementation(NativeMethod.RuntimeHelpersInitializeArray)]
        public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
        {
            throw new NotImplementedException();
        }

        [ArduinoImplementation(NativeMethod.RuntimeHelpersRunClassConstructor)]
        public static void RunClassConstructor(RuntimeTypeHandle rtHandle)
        {
            throw new NotImplementedException();
        }

        public static int OffsetToStringData
        {
            get
            {
                // TODO: Will depend on our string implementation
                return 8;
            }
        }

        [ArduinoImplementation(NativeMethod.RuntimeHelpersGetHashCode)]
        public static int GetHashCode(object? obj)
        {
            return 0;
        }

        public static bool IsReferenceOrContainsReferences<T>()
        {
            return IsReferenceOrContainsReferencesCore(typeof(T));
        }

        [ArduinoImplementation((NativeMethod.RuntimeHelpersIsReferenceOrContainsReferencesCore))]
        private static bool IsReferenceOrContainsReferencesCore(Type t)
        {
            throw new NotImplementedException();
        }

        internal static bool IsBitwiseEquatable<T>()
        {
            return IsBitwiseEquatableCore(typeof(T));
        }

        [ArduinoImplementation(NativeMethod.RuntimeHelpersIsBitwiseEquatable)]
        private static bool IsBitwiseEquatableCore(Type t)
        {
            throw new NotImplementedException();
        }

        [ArduinoImplementation(NativeMethod.RuntimeHelpersGetMethodTable)]
        public static unsafe void* GetMethodTable(object obj)
        {
            throw new NotImplementedException();
        }

        internal static unsafe ref int GetMultiDimensionalArrayBounds(Array array)
        {
            throw new NotImplementedException();
        }

        internal static unsafe int GetMultiDimensionalArrayRank(Array array)
        {
            throw new NotImplementedException();
        }

        [ArduinoImplementation(NativeMethod.RuntimeHelpersGetRawArrayData)]
        internal static unsafe ref byte GetRawArrayData(this Array array)
        {
            throw new NotImplementedException();
        }

        internal static ref byte GetRawData(this object obj) =>
            ref MiniUnsafe.As<RawData>(obj).Data;

        // Helper class to assist with unsafe pinning of arbitrary objects.
        // It's used by VM code (for what?)
        internal class RawData
        {
            public byte Data;
        }

        [ArduinoImplementation]
        internal static new bool Equals(object? o1, object? o2)
        {
            if (ReferenceEquals(o1, o2))
            {
                return true;
            }

            if (o1 == null || o2 == null)
            {
                return false;
            }

            return o1.Equals(o2);
        }
    }
}
