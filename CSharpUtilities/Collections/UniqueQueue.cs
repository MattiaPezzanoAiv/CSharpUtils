using System.Collections.Generic;

namespace MP.CSharpUtilities.Collections
{
    public class UniqueQueue<T> : BaseUniqueCollection<T>
    {
        private readonly Queue<T> m_queue;

        public UniqueQueue(int capacity = 0) : base(capacity)
        {
            m_queue = new Queue<T>(capacity);
        }

        public bool Enqueue(T item)
        {
            if(TryAdd(item))
            {
                m_queue.Enqueue(item);
                return true;
            }

            return false;
        }

        public T Dequeue()
        {
            if(Count == 0)
            {
                throw new System.InvalidOperationException($"Trying to dequeue on an empty {typeof(UniqueQueue<T>).Name}");
            }

            var item = m_queue.Dequeue();
            TryRemove(item);
            return item;
        }
    }
}
