namespace Jwc.Funz
{
    public interface IRegistration : IOwned
    {
        IOwned ReusedWithinNone();
        IOwned ReusedWithinContainer();
        IOwned ReusedWithinHierarchy();
        IOwned ReusedWithin(object scope);
    }

    public interface IOwned
    {
        void OwnedByContainer();
        void OwnedByExternal();
    }
}