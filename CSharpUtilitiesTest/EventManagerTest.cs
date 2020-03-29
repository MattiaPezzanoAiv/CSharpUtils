using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CSharpUtilities.Events;

namespace CSharpUtilitiesTest
{
    [TestFixture]
    public class EventManagerTest
    {
        public interface ITestEvent : IEventHandler
        {
            void TestFunc();
        }

        public interface IGlobalTestEvent : IGlobalEventHandler
        {
            void TestFunc();
            int GetInt();
        }

        public class TestClass : ITestEvent
        {
            public int Called { get; set; }
            
            public void TestFunc()
            {
                Called++;
            }
        }

        public class GlobalTestClass : IGlobalTestEvent
        {
            public int Called { get; set; }

            public void TestFunc()
            {
                Called++;
            }

            public int GetInt()
            {
                return Called;
            }
        }

        [Test]
        public void EventManagerHandler()
        {
            var item = new TestClass();

            EventManager.Raise<ITestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(0));

            EventManager.RegisterHandler<ITestEvent>(item);
            EventManager.Raise<ITestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
            EventManager.UnregisterHandler<ITestEvent>(item);

            EventManager.Raise<ITestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
        }

        [Test]
        public void EventManagerKeyedHandlerGreen()
        {
            var item = new TestClass();
            var key = 1;

            EventManager.Raise<ITestEvent>(key, o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(0));

            EventManager.RegisterHandler<ITestEvent>(key, item);
            EventManager.Raise<ITestEvent>(key, o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
            EventManager.UnregisterHandler<ITestEvent>(key, item);

            EventManager.Raise<ITestEvent>(key, o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
        }

        [Test]
        public void EventManagerKeyedHandlerRed()
        {
            var item = new TestClass();
            var key = 1;
            var wrongKey = 2;

            EventManager.Raise<ITestEvent>(key, o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(0));

            EventManager.RegisterHandler<ITestEvent>(key, item);
            EventManager.Raise<ITestEvent>(wrongKey, o => o.TestFunc());
            EventManager.Raise<ITestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(0));
            EventManager.UnregisterHandler<ITestEvent>(key, item);
        }

        [Test]
        public void EventManagerGlobal()
        {
            var item = new GlobalTestClass();

            EventManager.RaiseGlobal<IGlobalTestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(0));

            EventManager.RegisterGlobalHandler<IGlobalTestEvent>(item);
            Assert.Throws<ArgumentException>(() => EventManager.RegisterGlobalHandler<IGlobalTestEvent>(item));

            EventManager.RaiseGlobal<IGlobalTestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
            EventManager.UnregisterGlobalHandler<IGlobalTestEvent>(item);

            EventManager.RaiseGlobal<IGlobalTestEvent>(o => o.TestFunc());
            Assert.That(item.Called, Is.EqualTo(1));
        }

        [Test]
        public void RaiseNullEx()
        {
            Assert.Throws<ArgumentNullException>(() => EventManager.Raise<ITestEvent>(null));
            Assert.Throws<ArgumentNullException>(() => EventManager.Raise<ITestEvent>(0, null));
            Assert.Throws<ArgumentNullException>(() => EventManager.RaiseGlobal<IGlobalTestEvent>(null));
            Assert.Throws<ArgumentNullException>(() => EventManager.RaiseWithResult<IGlobalTestEvent, int>(null));
        }

        // todo test raise with result
        [Test]
        public void RaiseResult()
        {
            var item = new GlobalTestClass();
        }
    }
}
