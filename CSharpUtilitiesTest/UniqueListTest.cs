using System;
using MP.CSharpUtilities.Collections;
using NUnit.Framework;

namespace CSharpUtilitiesTest
{
    [TestFixture]
    public class UniqueListTest
    {
        [Test]
        public void UniqueListAdd()
        {
            UniqueList<int> list = new UniqueList<int>();
            Assert.That(list.Count, Is.EqualTo(0));
            list.Add(10);
            Assert.That(list.Count, Is.EqualTo(1));
        }

        [Test]
        public void UniqueListRemove()
        {
            UniqueList<int> list = new UniqueList<int>();
            Assert.That(list.Remove(10), Is.False);
            list.Add(10);
            Assert.That(list.Remove(10), Is.True);
        }

        [Test]
        public void UniqueListRemoveAtEx()
        {
            UniqueList<int> list = new UniqueList<int>();
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(0));
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
            list.Add(10);
            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(1));
            Assert.That(() => list.RemoveAt(0), Throws.Nothing);
        }

        [Test]
        public void UniqueListForeach()
        {
            UniqueList<int> list = new UniqueList<int>();
            list.Add(1);
            list.Add(2);

            foreach (var item in list)
            {
                Console.WriteLine("item is " + item);
            }
        }
    }
}
