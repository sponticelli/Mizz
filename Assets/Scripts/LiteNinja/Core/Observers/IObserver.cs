namespace LiteNinja.Core.Observers
{
    public interface IObserver
    {
        void OnChange(IObservable observable);
    }
}