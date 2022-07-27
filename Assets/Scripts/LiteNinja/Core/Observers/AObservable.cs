using System;
using System.Collections.Generic;

namespace LiteNinja.Core.Observers
{
    [Serializable]
    public abstract class AObservable : IObservable

    {
        [NonSerialized] private readonly List<IObserver> _observers;
        private int _count;

        protected AObservable()
        {
            _observers = new List<IObserver>();
        }

        public bool IsChanged { get; private set; }

        public void Attach(IObserver observer)
        {
            var index = _observers.IndexOf(observer);
            if (index == -1)
            {
                _observers.Add(observer);
                _count++;
                OnObserversChanged(_count);
            }
            else
            {
                if (_count == 1) return;
                _observers[index] = null;
                _observers.Add(observer);
            }
        }

        public void Detach(IObserver observer)
        {
            var index = _observers.IndexOf(observer);
            if (index == -1) return;
            _observers[index] = null;
            _count--;
            OnObserversChanged(_count);
        }

        public void DetachAll()
        {
            _observers.Clear();
            _count = 0;
            OnObserversChanged(_count);
        }

        public void Notify()
        {
            IsChanged = true;

            if (_count == 0)
                return;

            var length = _observers.Count;
            for (var i = 0; i < Math.Min(length, _observers.Count); i++)
            {
                var current = _observers[i];
                current?.OnChange(this);
            }

            RemoveNullObservers();
        }

        public void Commit()
        {
            IsChanged = false;
        }

        public void SetChangedAndCommit()
        {
            Notify();
            Commit();
        }


        protected virtual void OnObserversChanged(int count)
        {
        }

        private void RemoveNullObservers()
        {
            if (_count == _observers.Count)
                return;
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                if (null == _observers[i])
                {
                    _observers.RemoveAt(i);
                }
            }
        }
    }
}