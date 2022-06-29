using System;

namespace Northwind.Services
{
    public static class ServiceResolver
    {
        private static IServiceProvider _provider;
        public static void Setup(IServiceProvider provider)
        {
            _provider = provider;
        }

        public static T GetService<T>()
        {
            return (T)_provider.GetService(typeof(T));
        }
    }
}
