using System;
using System.Runtime.CompilerServices;
using Reloaded.Memory;
using Reloaded.Memory.Kernel32;
using Reloaded.Memory.Sources;
using ReloadedRPCS3TestsNoConfig.RPCS3;

namespace ReloadedRPCS3TestsNoConfig
{
    public unsafe class RPCS3VirtualMemory : IMemory
    {
        private readonly RPCS3Bindings mBindings;

        public RPCS3VirtualMemory( RPCS3Bindings bindings )
        {
            mBindings = bindings;
        }

        public IntPtr GetPtr( uint memoryAddress )
        {
            return ( IntPtr )( ( long )mBindings.vm_g_base_addr + ( long )memoryAddress );
        }

        public IntPtr GetPtr( IntPtr memoryAddress )
        {
            return ( IntPtr )( ( long )mBindings.vm_g_base_addr + ( long )memoryAddress );
        }

        public void Read<T>( uint memoryAddress, out T value ) where T : unmanaged
            => Read( ( IntPtr )memoryAddress, out value );

        public void Read<T>( IntPtr memoryAddress, out T value ) where T : unmanaged
            => Read( memoryAddress, out value, false );

        public void Read<T>( IntPtr memoryAddress, out T value, bool marshal )
            => Memory.Instance.Read<T>( GetPtr( memoryAddress ), out value, marshal );

        public void ReadRaw( uint memoryAddress, out byte[] value, int length )
            => ReadRaw( memoryAddress, out value, length );

        public void ReadRaw( IntPtr memoryAddress, out byte[] value, int length )
            => Memory.Instance.ReadRaw( GetPtr( memoryAddress ), out value, length );

        public void Write<T>( uint memoryAddress, ref T item ) where T : unmanaged
            => Write( ( IntPtr )memoryAddress, ref item );

        public void Write<T>( IntPtr memoryAddress, ref T item ) where T : unmanaged
            => Write<T>( memoryAddress, ref item, false );

        public void Write<T>( IntPtr memoryAddress, ref T item, bool marshal )
            => Memory.Instance.Write( GetPtr( memoryAddress ), ref item, marshal );

        public void WriteRaw( IntPtr memoryAddress, byte[] data )
            => Memory.Instance.WriteRaw( GetPtr( memoryAddress ), data );

        public IntPtr Allocate( int length )
        {
            return (IntPtr)( mBindings.vm_alloc.GetWrapper()( ( uint )length, 0 ) );
        }

        public bool Free( IntPtr address )
        {
            return mBindings.vm_dealloc.GetWrapper()( (uint)address, 0 ) != 0;
        }

        public Kernel32.MEM_PROTECTION ChangePermission( IntPtr memoryAddress, int size, Kernel32.MEM_PROTECTION newPermissions )
            => Memory.Instance.ChangePermission( memoryAddress, size, newPermissions );
    }
}
