using System.ComponentModel;

namespace Jwc.Funz
{
    /// <summary>
    /// Fluent API that allows specifying the reuse instances.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReused : IFluentInterface
    {
        /// <summary>
        /// Specifies each request to resolve the dependency will result in a new
        /// instance being returned.
        /// </summary>
        /// <returns>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </returns>
        IOwned ReusedWithinNone();

        /// <summary>
        /// Specifies instances are reused only at the given container. Descendent
        /// containers do not reuse parent container instances and get
        /// a new instance at their level.
        /// </summary>
        /// <returns>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </returns>
        IOwned ReusedWithinContainer();

        /// <summary>
        /// Specifies instances are reused within a container hierarchy. Instances
        /// are created (if necessary) in the container where the registration
        /// was performed, and are reused by all descendent containers.
        /// </summary>
        /// <returns>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </returns>
        IOwned ReusedWithinHierarchy();

        /// <summary>
        /// Specifies instances are reused within the given scope. To reuse instances,
        /// a container should have same scope with the given scope, which can be
        /// passed when constructed.
        /// </summary>
        /// <param name="scope">
        /// The custom scope, within which instances are reused.
        /// </param>
        /// <returns>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </returns>
        IOwned ReusedWithin(object scope);
    }
}