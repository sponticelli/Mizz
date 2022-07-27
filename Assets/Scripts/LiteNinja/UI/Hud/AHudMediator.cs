using System;

namespace LiteNinja.UI.Hud
{
    public abstract class AHudMediator<T> : AUIMediator where T : IHud
    {
        private bool _isShowed;
        protected T _view;

        public override Type ViewType => typeof(T);
        public T View => _view;

        public sealed override void Mediate(object view)
        {
            _view = (T) view;
            _isShowed = false;
        }

        public sealed override void Disintermediate()
        {
            if (_isShowed)
            {
                Hide();
            }
            _view = default(T);
        }

        public sealed override void Show()
        {
            _view.IsActive = true;
            InternalShow();
        }

        public sealed override void Hide()
        {
            _view.IsActive = false;
            InternalHide();
        }

        protected abstract void InternalShow();
        protected abstract void InternalHide();
    }
}