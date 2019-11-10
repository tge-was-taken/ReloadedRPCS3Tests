namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdPair<T1, T2>
        where T1 : unmanaged
        where T2 : unmanaged
    {
        public T1 First;
        public T2 Second;
    }
}
