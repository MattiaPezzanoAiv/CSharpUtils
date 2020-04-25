using System;
using NUnit.Framework;
using MP.CSharpUtilities.ObjectPool;

namespace CSharpUtilitiesTest
{
    [TestFixture]
    public class SimplePoolTest
    {
        private class TestPoolable { 
            
            public static void Destroy(ref TestPoolable item)
            {
                item = null;
            }
        }

        [Test]
        public void SimplePool()
        {
            SimplePool<TestPoolable> pool = new SimplePool<TestPoolable>(10);
            Assert.That(pool.Available, Is.EqualTo(0));

            var item = pool.Get();
            Assert.That(item, Is.Not.Null);
            Assert.That(() => pool.Recycle(item), Throws.Nothing);
            Assert.That(pool.Available, Is.EqualTo(1));
            Assert.Throws<InvalidOperationException>(() => pool.Recycle(item));
            Assert.Throws<ArgumentNullException>(() => pool.Recycle(null));
            Assert.That(pool.Available, Is.EqualTo(1));
        }
    }
}
