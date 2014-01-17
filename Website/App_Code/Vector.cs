using System;
using System.Collections.Generic;
// Class taken from SIT221 assignments -- Created by Justin Rough
namespace Vector
{
    public class Vector<TYPE>
        : IEnumerable<TYPE>
    {

        #region Constants
        private const int DEFAULT_CAPACITY = 4;
        #endregion
        #region Private Data
        private TYPE[] _Array;
        private int _LastUsed = -1;
        private int _Version = 0;
        #endregion
        #region Public Properties
        public TYPE this[int index]
        {
            get
            {
                if (index < 0 || index > _LastUsed)
                    throw new IndexOutOfRangeException();
                return _Array[index];
            }
            set
            {
                if (index < 0 || index > _LastUsed)
                    throw new IndexOutOfRangeException();
                _Array[index] = value;
                _Version++;
            }
        }

        public int Capacity
        {
            get { return _Array.Length; }
            set
            {
                if (value != _Array.Length)
                {
                    if (value < _LastUsed + 1)
                        throw new ArgumentOutOfRangeException("Capacity");
                    else if (value >= _LastUsed + 1)
                        SetArraySize(value);
                }
            }
        }

        public int Count
        {
            get { return _LastUsed + 1; }
        }
        #endregion
        #region Enumerator Support
        public class VectorEnumerator : IEnumerator<TYPE>
        {
            private Vector<TYPE> _Vector;
            private int _Version;
            private int _Index;
            private TYPE _Current;
            internal VectorEnumerator(Vector<TYPE> vector)
            {
                _Vector = vector;
                _Version = vector._Version;
                Reset();
            }

            public TYPE Current
            {
                get { return _Current; }
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return _Current; }
            }

            public bool MoveNext()
            {
                if (_Version != _Vector._Version)
                    throw new InvalidOperationException("Vector changed during enumeration");

                if (_Index < _Vector._LastUsed)
                {
                    _Current = _Vector._Array[++_Index];
                    return true;
                }
                else
                {
                    _Current = default(TYPE);
                    return false;
                }
            }

            public void Reset()
            {
                if (_Version != _Vector._Version)
                    throw new InvalidOperationException("Vector changed during enumeration");

                _Index = -1;
                _Current = default(TYPE);
            }
        }

        public IEnumerator<TYPE> GetEnumerator()
        {
            return new VectorEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new VectorEnumerator(this);
        }
        #endregion
        #region Constructors
        public Vector()
            : this(DEFAULT_CAPACITY)
        {
        }

        public Vector(int capacity)
        {
            _Array = new TYPE[capacity];
        }
        #endregion

        #region Public Methods
        public void Add(TYPE element)
        {
            if (_LastUsed + 1 == _Array.Length)
            {
                SetArraySize(_Array.Length * 2);
            }

            _Array[++_LastUsed] = element;
            _Version++;
        }

        public void Clear()
        {
            _Array = new TYPE[DEFAULT_CAPACITY];
            _LastUsed = -1;
            _Version = 0;
        }

        public bool Contains(TYPE targetValue)
        {
            // PROBABILITY AND SENTINEL SEARCH OPTIMISATION
            Add(targetValue);
            _Version--;

            int targetIndex = 0;
            while (!_Array[targetIndex].Equals(targetValue))
                targetIndex++;

            if (targetIndex != _LastUsed && targetIndex > 0)
            {
                Swap(ref _Array[targetIndex - 1], ref _Array[targetIndex]);
                targetIndex--;
            }

            _Array[_LastUsed] = default(TYPE);
            _LastUsed--;

            if (targetIndex == _LastUsed + 1)
                return false;
            else
                return true;

            #region Optimisations
            // PROBABILITY SEARCH OPTIMISATION
            /*int targetIndex = -1;

            for (int i = 0; i <= _LastUsed; i++)
            {
                if (_Array[i].Equals(targetValue))
                {
                    targetIndex = i;
                    break;
                }
            }

            if (targetIndex > 0)
            {
                Swap(ref _Array[targetIndex - 1], ref _Array[targetIndex]);
                targetIndex--;
            }

            if (targetIndex == -1)
                return false;
            else
                return true;*/

            // SENTINEL SEARCH OPTIMISATION
            /*Add(targetValue);
            
            int targetIndex = 0;
            while (!_Array[targetIndex].Equals(targetValue))
            {
                targetIndex++;
            }

            _Array[_LastUsed] = default(TYPE);
            _LastUsed--;

            if (targetIndex == _LastUsed + 1)
                return false;
            else
                return true;*/
            #endregion
        }
        #endregion
        #region Private Methods
        private static void Swap(ref TYPE a, ref TYPE b)
        {
            TYPE temp = a;
            a = b;
            b = temp;
        }
        private void SetArraySize(int newSize)
        {
            TYPE[] newArray = new TYPE[newSize];
            int length = (_Array.Length < newSize ? _Array.Length : newSize);
            for (int i = 0; i < length; i++)
                newArray[i] = _Array[i];
            _Array = newArray;
        }
        #endregion

    }
}
