using System;
using System.Collections.Generic;

namespace MP.CSharpUtilities.Collections
{
    public sealed class UniqueList<T> : BaseUniqueCollection<T>
    {
        private readonly List<T> m_list;
        
        public UniqueList(int capacity = 0) : base(capacity)
        {
            m_list = new List<T>(capacity);
        }

        public T this[int index]
        {
            get => m_list[index];
            set
            {
                m_list[index] = value;
            }
        }

        public bool Add(T item)
        {
            if (TryAdd(item))
            {
                m_list.Add(item);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_list.Clear();
            m_hashSet.Clear();
        }

        public bool Contains(T item) => m_hashSet.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => m_list.GetEnumerator();

        public int IndexOf(T item) => m_list.IndexOf(item);

        public void Insert(int index, T item)
        {
            if(TryAdd(item))
            {
                m_list.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            if (TryRemove(item))
            {
                m_list.Remove(item);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if(index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException($"Index requested is: {index}. Max index is {Count - 1}");
            }

            var item = m_list[index];
            Remove(item);
        }
    }
}
