using System;
using System.Collections.Generic;

namespace CSharpUtilities.Collections
{
    public class BaseUniqueCollection<T>
    {
        protected readonly HashSet<T> m_hashSet;

        public int Count => m_hashSet.Count;

        public bool IsReadOnly => false;

        public BaseUniqueCollection(int capacity = 0)
        {
            m_hashSet = new HashSet<T>(capacity);
        }

        protected bool TryAdd(T item)
        {
            return m_hashSet.Add(item);
        }

        protected bool TryRemove(T item)
        {
            return m_hashSet.Remove(item);
        }
    }
}
