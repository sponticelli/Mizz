using System;

namespace LiteNinja.DI
{
    public interface IDIContainer
    {
        void Bind(params object[] objects);
        void Bind(object obj, Type type);
        void Resolve();
        void Unbind(params object[] objects);
        T Get<T>() where T : class;
        object Get(Type type);
    }
}