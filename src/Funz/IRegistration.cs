using System.ComponentModel;

namespace Jwc.Funz
{
    /// <summary>
    /// Fluent API for customizing the registration of a service.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IRegistration : IReused, IOwned
    {
    }
}