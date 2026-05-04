using System;
using System.Collections.Generic;

namespace WarAquaDrone.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service) where T : class => Services[typeof(T)] = service;

        public static T Get<T>() where T : class
        {
            if (Services.TryGetValue(typeof(T), out var service)) return (T)service;
            return null;
        }

        public static void Clear() => Services.Clear();
    }
}
