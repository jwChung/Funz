namespace Jwc.Funz
{
    public interface IRegistration : IOwned
    {
        IOwned ReusedWithinNone();
        IOwned ReusedWithinContainer();
        IOwned ReusedWithinHierarchy();
        IOwned ReusedWithin(object scope);
    }
}