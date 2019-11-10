using Reloaded.Memory.Sources;
using ReloadedRPCS3TestsNoConfig.Utilities;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdString : IDisposable
    {
        private const int RESERVED_SIZE = 16;

        public fixed byte Reserved[RESERVED_SIZE];
        public SizeT Length;
        public SizeT Capacity;

        public IntPtr DataPtr
        {
            get
            {
                fixed ( StdString* pThis = &this )
                    return GetDataPtr( pThis );
            }
        }

        public byte[] Data
        {
            get
            {
                TryGetData( out var data );
                return data;
            }
        }

        public bool TryGetData(out byte[] data )
        {
            fixed ( StdString* pThis = &this )
            {
                var dataPtr = GetDataPtr( pThis );
                if ( dataPtr == null )
                {
                    data = null;
                    return true;
                }

                if ( Length == 0 )
                {
                    data = new byte[0];
                    return true;
                }

                if ( MemoryHelper.TryReadBytes( dataPtr, ( int )Length, out data ) )
                {
                    return true;
                }
                else
                {
                    data = null;
                    return false;
                }
            }
        }

        public IntPtr GetDataPtr( StdString* pStr )
        {
            return Length < RESERVED_SIZE ? ( IntPtr )pStr : ( IntPtr )( *( long* )pStr );
        }

        public void Dispose()
        {
            if ( Length < RESERVED_SIZE )
                return;

            if ( DataPtr != null && IsValid() )
            {
                Memory.Instance.Free( DataPtr );

                for ( int i = 0; i < RESERVED_SIZE; i++ )
                    Reserved[i] = 0;

                Length = 0;
                Capacity = 0;
            }
        }

        public bool IsValid()
        {
            return MemoryHelper.TryReadBytes( DataPtr, ( int )Length, out _ );
        }

        public override string ToString()
        {
            return ToString( Encoding.ASCII );
        }

        public string ToString( Encoding encoding )
        {
            if ( !TryGetData( out var data ) )
            {
                return "<< Data is unreadable >>";
            }
            else
            {
                if ( data == null )
                    return null;
                else if ( data.Length == 0 )
                    return string.Empty;
                else
                    return encoding.GetString( data );
            }
        }

        public static StdString FromString( string value, Encoding encoding )
        {
            var bytes = encoding.GetBytes(value);
            var str = new StdString { Length = bytes.Length, Capacity = bytes.Length + 1 };

            if ( value.Length < RESERVED_SIZE )
            {
                Unsafe.Copy( &str, ref bytes[0] );
            }
            else
            {
                var data = Memory.Instance.Allocate( (int)str.Capacity );
                *( ( long* )&str ) = data.ToInt64();
            }

            return str;
        }

        public static implicit operator StdString( string value )
        {
            return FromString( value, Encoding.ASCII );
        }

        public static implicit operator string( StdString value )
        {
            return value.ToString();
        }
    }
}
