using System;
using NUnit.Framework;
using MP.CSharpUtilities.ObjectPool;

namespace CSharpUtilitiesTest
{
    public struct PoolableTestAllocator : TPoolAllocator<PoolableTest>
    {
        public PoolableTest CreateNew()
        {
            return new PoolableTest();
        }
    }

    public class PoolableTest : IPoolable
    {
        public bool GetCalled { get; set; }
        public bool RecycleCalled { get; set; }

        public void OnGet()
        {
            GetCalled = true;
        }

        public void OnRecycle()
        {
            RecycleCalled = true;
        }
    }

    [TestFixture]
    public class PoolTest
    {
        [Test]
        public void Pool()
        {
            var pool = new Pool<PoolableTest, PoolableTestAllocator>(0);

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
