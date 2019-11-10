namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdTreeNode<TValue>
        where TValue : unmanaged
    {
        public StdTreeNode<TValue>* Left;
        public StdTreeNode<TValue>* Parent;
        public StdTreeNode<TValue>* Right;
        public byte Color;
        public Bool IsNil;
        public fixed byte Padding[2];
        public TValue Value;
    }
}
