using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtilities.Events
{
    public static class EventManager
    {
        /// <summary>
        /// Contains the non keyed handlers
        /// </summary>
        private static readonly Dictionary<Type, List<IEventHandler>> m_handlers =
            new Dictionary<Type, List<IEventHandler>>();

        /// <summary>
        /// Contains the keyed handlers
        /// </summary>
        private static readonly Dictionary<int, Dictionary<Type, List<IEventHandler>>> m_keyedHandlers =
            new Dictionary<int, Dictionary<Type, List<IEventHandler>>>();

        /// <summary>
        /// Contains the global handlers
        /// </summary>
        private static readonly Dictionary<Type, IGlobalEventHandler> m_globalHandlers =
            new Dictionary<Type, IGlobalEventHandler>();

        public static void RegisterHandler<T>(T handler) where T : IEventHandler
        {
            var type = typeof(T);

            List<IEventHandler> list;
            if (!m_handlers.TryGetValue(type, out list))
            {
                // if not present, add a new list
                list = new List<IEventHandler>();
                m_handlers[type] = list;
            }

            list.Add(handler);
        }

        public static void RegisterHandler<T>(int key, T handler) where T : IEventHandler
        {
            var type = typeof(T);

            Dictionary<Type, List<IEventHandler>> dic;
            List<IEventHandler> list;
            if (!m_keyedHandlers.TryGetValue(key, out dic))
            {
                dic = new Dictionary<Type, List<IEventHandler>>();
                m_keyedHandlers[key] = dic;

                list = new List<IEventHandler>();
                dic[type] = list;
            }
            else if (!dic.TryGetValue(type, out list))
            {
                list = new List<IEventHandler>();
                dic[type] = list;
            }

            list.Add(handler);
        }

        public static void RegisterGlobalHandler<T>(T globalHandler) where T : IGlobalEventHandler
        {
            var type = typeof(T);

            if (!m_globalHandlers.ContainsKey(type))
            {
                // if not present, add a new list
                m_globalHandlers[type] = globalHandler;
            }
            else
            {
                throw new ArgumentException("A global handler is already registered for the type " + type.Name);
            }
        }

        public static void UnregisterHandler<T>(T handler) where T : IEventHandler
        {
            var type = typeof(T);

            if (m_handlers.TryGetValue(type, out var list))
            {
                list.Remove(handler);
            }
        }

        public static void UnregisterHandler<T>(int key, T handler) where T : IEventHandler
        {
            var type = typeof(T);

            if (m_keyedHandlers.TryGetValue(key, out var dic)
                && dic.TryGetValue(type, out var list))
            {
                list.Remove(handler);
            }
        }

        public static void UnregisterGlobalHandler<T>(T globalHandler) where T : IGlobalEventHandler
        {
            var type = typeof(T);

            if (m_globalHandlers.ContainsKey(type))
            {
                m_globalHandlers.Remove(type);
            }
        }

        public static void Raise<T>(Action<T> action) where T : IEventHandler
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }

            var type = typeof(T);

            if (m_handlers.TryGetValue(type, out var list))
            {
                foreach (var handler in list)
                {
                    action((T)handler);
                }
            }
        }

        public static void Raise<T>(int key, Action<T> action) where T : IEventHandler
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }

            var type = typeof(T);

            if (m_keyedHandlers.TryGetValue(key, out var dic)
                && dic.TryGetValue(type, out var list))
            {
                foreach (var handler in list)
                {
                    action((T)handler);
                }
            }
        }

        public static void RaiseGlobal<T>(Action<T> action) where T : IGlobalEventHandler
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }

            var type = typeof(T);

            if (m_globalHandlers.TryGetValue(type, out var handler))
            {
                action((T)handler);
            }
        }

        public static TResult RaiseWithResult<T, TResult>(Func<T, TResult> action) where T : IGlobalEventHandler
        {
            if (action == null)
            {
                throw new ArgumentNullException();
            }

            var type = typeof(T);
            if (m_globalHandlers.TryGetValue(type, out var handler))
            {
                return action((T)handler);
            }

            return default;
        }
    }
}
