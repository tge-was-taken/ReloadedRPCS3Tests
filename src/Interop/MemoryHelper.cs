using System;
using System.Diagnostics;
using Reloaded.Memory.Kernel32;

namespace ReloadedRPCS3TestsNoConfig.Utilities
{
    public static unsafe class MemoryHelper
    {
        public static bool TryReadBytes( IntPtr memoryAddress, int length, out byte[] value )
        {
            value = new byte[length];
            fixed ( byte* bufferPtr = value )
            {
                return Kernel32.ReadProcessMemory( Process.GetCurrentProcess().Handle, ( IntPtr )memoryAddress, ( IntPtr )bufferPtr, ( UIntPtr )value.Length, out _ );
            }
        }
    }
}
