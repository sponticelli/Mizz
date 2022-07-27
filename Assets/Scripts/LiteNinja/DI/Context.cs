using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteNinja.DI
{
    public class DIContainer : IDisposable, IDIContainer
    {
        private readonly Dictionary<Type, object> _map;

        public DIContainer()
        {
            _map = new Dictionary<Type, object>(100)
            {
                [typeof(DIContainer)] = this
            };
        }

        public DIContainer(DIContainer parent)
        {
            _map = new Dictionary<Type, object>(parent._map)
            {
                [typeof(DIContainer)] = this
            };
        }

        public void Dispose()
        {
            foreach (var item in _map.Where(item => this != item.Value))
            {
                (item.Value as IDisposable)?.Dispose();
            }

            _map.Clear();
        }

        public void Bind(params object[] objects)
        {
            foreach (var obj in objects)
            {
                Bind(obj, obj.GetType());
            }
        }

        public void Bind(object obj, Type type)
        {
            _map[type] = obj;
        }

        public void Resolve()
        {
            var injector = Get<Injector>();
            foreach (var obj in _map.Values)
            {
                injector.Inject(obj);
            }
        }

        public void Unbind(params object[] objects)
        {
            foreach (var obj in objects)
            {
                _map.Remove(obj.GetType());
            }
        }

        public T Get<T>() where T : class
        {
#if UNITY_EDITOR
            if (!_map.ContainsKey(typeof(T)))
            {
                throw new KeyNotFoundException("Not found " + typeof(T));
            }
#endif

            return (T)_map[typeof(T)];
        }

        public object Get(Type type)
        {
#if UNITY_EDITOR
            if (!_map.ContainsKey(type))
            {
                throw new KeyNotFoundException("Not found " + type);
            }
#endif
            return _map[type];
        }
    }
}