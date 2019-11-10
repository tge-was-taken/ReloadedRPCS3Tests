namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public struct Bool
    {
        public byte Value;

        public static implicit operator Bool( bool value )
        {
            return new Bool() { Value = ( byte )( value ? 1 : 0 ) };
        }

        public static implicit operator Bool( int value )
        {
            return new Bool() { Value = ( byte )( value != 0 ? 1 : 0 ) };
        }

        public static implicit operator bool( Bool value )
        {
            return value.Value != 0;
        }

        public override string ToString()
        {
            return ( ( bool )this ).ToString();
        }
    }
}
