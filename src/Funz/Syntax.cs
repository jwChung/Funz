namespace Jwc.Funz
{
    public interface IRegistration
    {
        void ReusedWithinNone();
        void ReusedWithinContainer();
        void ReusedWithinHierarchy();
        void ReusedWithin(object scope);
    }
}