using System;
using MP.CSharpUtilities.Collections;
using NUnit.Framework;

namespace CSharpUtilitiesTest
{
    [TestFixture]
    public class UniqueQueueTest
    {
        [Test]
        public void UniqueQueueEnqueue()
        {
            UniqueQueue<int> queue = new UniqueQueue<int>();
            Assert.That(queue.Enqueue(10), Is.True);
            Assert.That(queue.Enqueue(10), Is.False);
        }

        [Test]
        public void UniqueQueueDequeue()
        {
            UniqueQueue<int> queue = new UniqueQueue<int>();
            queue.Enqueue(10);
            Assert.That(queue.Dequeue(), Is.EqualTo(10));
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }
}
