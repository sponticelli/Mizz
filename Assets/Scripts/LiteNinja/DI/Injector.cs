using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LiteNinja.DI
{
    public sealed class Injector
    {
        private readonly Dictionary<Type, FieldInfo[]> _fieldsCache;
        private readonly IDIContainer _diContainer;

        public Injector(IDIContainer diContainer)
        {
            _diContainer = diContainer;
            _fieldsCache = new Dictionary<Type, FieldInfo[]>(100);
        }

        public void Inject(object value)
        {
            if (null == value)
                return;

            var type = value.GetType();
            TryToMapFields(type);

            var fields = _fieldsCache[type];
            foreach (var fieldInfo in fields)
            {
                fieldInfo.SetValue(value, _diContainer.Get(fieldInfo.FieldType));
            }
        }

        public T Get<T>() where T : class
        {
            return _diContainer.Get<T>();
        }

        private void TryToMapFields(Type type)
        {
            if (_fieldsCache.ContainsKey(type))
                return;

            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            fields = fields.Where(temp => temp.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0).ToArray();

            _fieldsCache[type] = fields;
        }
    }
}