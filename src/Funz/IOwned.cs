using System.ComponentModel;

namespace Jwc.Funz
{
    /// <summary>
    /// Fluent API that allows specifying the owner of instances
    /// created from a registration.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IOwned : IFluentInterface
    {
        /// <summary>
        /// Specifies that container should dispose provided instances when it is disposed. (default)
        /// </summary>
        void OwnedByContainer();

        /// <summary>
        /// Specifies that container does not dispose provided instances.
        /// </summary>
        void OwnedByExternal();
    }
}