using System.ComponentModel;

namespace Jwc.Funz
{
    /// <summary>
    /// Fluent API that allows specifying the reuse instances.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReused
    {
        /// <summary>
        /// Specifies each request to resolve the dependency will result in a new 
        /// instance being returned.
        /// </summary>
        /// <return>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </return>
        IOwned ReusedWithinNone();

        /// <summary>
        /// Specifies instances are reused only at the given container. Descendent 
        /// containers do not reuse parent container instances and get  
        /// a new instance at their level.
        /// </summary>
        /// <return>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </return>
        IOwned ReusedWithinContainer();

        /// <summary>
        /// Specifies instances are reused within a container hierarchy. Instances 
        /// are created (if necessary) in the container where the registration
        /// was performed, and are reused by all descendent containers.
        /// </summary>
        /// <return>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </return>
        IOwned ReusedWithinHierarchy();


        /// <summary>
        /// Specifies instances are reused within the given scope. To reuse instainces,
        /// a container should have same scope with the given scope, which can be
        /// passed when constructed.
        /// </summary>
        /// <param name="scope">
        /// The custom scope, within which instances are reused.
        /// </param>
        /// <return>
        /// The fluent API instance about how to specifying the owner of instances.
        /// </return>
        IOwned ReusedWithin(object scope);
    }
}