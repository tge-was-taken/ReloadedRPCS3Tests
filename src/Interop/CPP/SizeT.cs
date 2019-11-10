using System;

namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct SizeT
    {
        public IntPtr Value;

        public static implicit operator int( SizeT value ) => value.Value.ToInt32();
        public static implicit operator long( SizeT value ) => value.Value.ToInt64();
        public static implicit operator SizeT( int value ) => new SizeT() { Value = new IntPtr( value ) };
        public static implicit operator SizeT( uint value ) => new SizeT() { Value = new IntPtr( value ) };
        public static implicit operator SizeT( long value ) => new SizeT() { Value = new IntPtr( value ) };
        public static implicit operator SizeT( ulong value ) => new SizeT() { Value = new IntPtr( ( long )value ) };
    }
}
