using System.Collections;
using System.Collections.Generic;

namespace ReloadedRPCS3TestsNoConfig.Interop.CPP
{
    public unsafe struct StdVector<T> : IList<T> where T : unmanaged
    {
        public void* Start;
        public void* End;
        public void* CapacityEnd;

        public T this[int index]
        {
            get => ( ( T* )Start )[index];
            set => ( ( T* )Start )[index] = value;
        }

        public int Count => ( int )( ( ( long )End - ( long )Start ) / sizeof( T ) );

        public bool IsReadOnly => false;

        public void Add( T item )
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains( T item )
        {
            return IndexOf( item ) != -1;
        }

        public void CopyTo( T[] array, int arrayIndex )
        {
            var count = Count;
            for ( int i = 0; i < count; i++ )
                array[arrayIndex + i] = this[i];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator( ref this );
        }

        public int IndexOf( T item )
        {
            var count = Count;
            for ( int i = 0; i < count; i++ )
            {
                if ( this[i].Equals( item ) )
                    return i;
            }

            return -1;
        }

        public void Insert( int index, T item )
        {
            throw new System.NotImplementedException();
        }

        public bool Remove( T item )
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt( int index )
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Enumerator : IEnumerator<T>
        {
            private StdVector<T> mVector;
            private int mCurrentIndex;

            public T Current => mVector[mCurrentIndex];

            object IEnumerator.Current => Current;

            public Enumerator(ref StdVector<T> vector)
            {
                mVector = vector;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if ( mVector.Count == 0 || mCurrentIndex == mVector.Count - 1 )
                    return false;

                ++mCurrentIndex;
                return true;
            }

            public void Reset()
            {
                mCurrentIndex = 0;
            }
        }
    }
}
