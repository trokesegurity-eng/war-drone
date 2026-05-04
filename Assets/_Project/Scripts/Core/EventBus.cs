using System;
using System.Collections.Generic;

namespace WarAquaDrone.Core
{
    public interface IGameEvent { }

    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> Handlers = new();

        public static void Subscribe<T>(Action<T> handler) where T : IGameEvent
        {
            var t = typeof(T);
            if (Handlers.TryGetValue(t, out var existing))
            {
                Handlers[t] = Delegate.Combine(existing, handler);
                return;
            }

            Handlers[t] = handler;
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : IGameEvent
        {
            var t = typeof(T);
            if (!Handlers.TryGetValue(t, out var existing)) return;

            var next = Delegate.Remove(existing, handler);
            if (next == null) Handlers.Remove(t);
            else Handlers[t] = next;
        }

        public static void Publish<T>(T evt) where T : IGameEvent
        {
            if (!Handlers.TryGetValue(typeof(T), out var del)) return;
            ((Action<T>)del)?.Invoke(evt);
        }
    }
}
