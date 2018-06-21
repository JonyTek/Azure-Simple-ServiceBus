using System;
using System.Collections.Generic;

namespace Azure.ServiceBus.Models
{
    public class MessageRouteTable
    {
        private readonly Dictionary<string, string> routes;

        public MessageRouteTable()
        {
            routes = new Dictionary<string, string>();
        }

        public MessageRouteTable AddRoute<T>(string path)
        {
            var name = typeof(T).FullName;

            if (name == null) throw new ArgumentException();

            routes.Add(name, path);

            return this;
        }

        public string GetRoute<T>(T instance)
        {
            var name = instance.GetType().FullName ?? throw new ArgumentException();

            return GetRoute(name);
        }

        public string GetRoute(string name)
        {
            return routes.ContainsKey(name) ? routes[name] : name;
        }
    }
}