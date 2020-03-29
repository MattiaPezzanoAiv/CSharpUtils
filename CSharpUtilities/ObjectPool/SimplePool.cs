using System;
using CSharpUtilities.Collections;

namespace CSharpUtilities.ObjectPool
{
    public sealed class SimplePool<T> where T : new()
    {
        private readonly UniqueQueue<T> m_items;

        public SimplePool(int capacity = 0)
        {
            m_items = new UniqueQueue<T>(capacity);
        }

        public int Available => m_items.Count;

        /// <summary>
        /// Get or create a new instance
        /// </summary>
        /// <returns>A valid instance of type <see cref="T"/></returns>
        public T Get()
        {
            T result;
            if(m_items.Count == 0)
            {
                result = new T();
            }
            else
            {
                result = m_items.Dequeue();
                if(result == null)
                {
                    throw new NullReferenceException("A null item has been found in the pool. " +
                        "That means the item has been modified or destroyed after recycle operation." +
                        "Be sure, after having recycled an item, to not access it anymore since its ownership" +
                        "can be assigned to any other object that requires it.");
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Recycle(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException();
            }

            if(!m_items.Enqueue(item))
            {
                throw new InvalidOperationException("This item has already been recycled");
            }
        }
    }
}
