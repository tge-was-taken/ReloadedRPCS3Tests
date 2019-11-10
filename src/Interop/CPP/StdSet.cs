using System.Collections;
using System.Collections.Generic;

namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdSet<TValue> : IStdTree
        where TValue : unmanaged
    {
        public StdTreeNode<TValue>* Head;
        public SizeT Size;

        public List<TValue> GetValues()
        {
            var list = new List<TValue>();

            void Traverse( StdTreeNode<TValue>* node )
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
