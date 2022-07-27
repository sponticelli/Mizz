using LiteNinja.Core.Observers;
using UnityEngine;

namespace LiteNinja.UI.Hud
{
    public abstract class AHud : MonoBehaviour, IHud
    {
        public bool IsActive
        {
            set => gameObject.SetActive(value);
        }

        protected abstract void OnEnable();
        protected abstract void OnDisable();
    }

    public abstract class AHud<T> : AHud, IObserver where T : IObservable
    {
        private T _model;

        public T Model
        {
            protected get { return _model; }
            set
            {
                if (null != _model)
                {
                    _model.Detach(this);
                }

                OnApplyModel(value);

                _model = value;

                if (null == _model) return;
                _model.Attach(this);
                OnModelChanged(_model);
            }
        }

        public void OnChange(IObservable observable)
        {
            if (observable is T)
            {
                OnModelChanged((T)observable);
            }
            else
            {
                OnModelChanged(Model);
            }
        }

        protected abstract void OnModelChanged(T model);

        protected virtual void OnApplyModel(T model)
        {
        }
    }
}