namespace LiteNinja.Core.Mediators
{
    public interface IMediator
    {
        void Mediate(object view);
        void Disintermediate();
    }
}