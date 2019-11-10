using System.Collections.Generic;

namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdMap<TKey, TValue> : IStdTree
        where TKey : unmanaged 
        where TValue : unmanaged
    {
        public StdTreeNode<StdPair<TKey, TValue>>* Head;
        public SizeT Size;

        public List<StdPair<TKey, TValue>> GetValues()
        {
            var list = new List<StdPair<TKey, TValue>>();

            void Traverse( StdTreeNode<StdPair<TKey, TValue>>* node )
            {
                if ( !node->IsNil )
                    list.Add( node->Value );

                var curNode = node->Left;
                while ( curNode != node && !curNode->IsNil )
                {
                    list.Add( curNode->Value );
                    curNode = curNode->Left;
                }

                curNode = node->Right;
                while ( curNode != node && !curNode->IsNil )
                {
                    list.Add( curNode->Value );
                    curNode = curNode->Right;
                }
            }

            Traverse( Head->Parent );

            return list;
        }
    }
}
