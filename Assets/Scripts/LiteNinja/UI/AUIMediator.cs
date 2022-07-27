using System;
using LiteNinja.Core.Mediators;

namespace LiteNinja.UI
{
    public abstract class AUIMediator : IMediator
    {
        public abstract Type ViewType { get; }

        public abstract void Mediate(object view);
        public abstract void Disintermediate();

        public abstract void Show();
        public abstract void Hide();
    }
}