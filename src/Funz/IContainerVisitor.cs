namespace Jwc.Funz
{
    /// <summary>
    /// Represents a contianer visitor.
    /// </summary>
    /// <typeparam name="TResult">A result type.</typeparam>
    public interface IContainerVisitor<out TResult>
    {
        /// <summary>
        /// Gets a value to indicating a result produced after visiting.
        /// </summary>
        TResult Result { get; }

        /// <summary>
        /// Visits a container.
        /// </summary>
        /// <param name="container">A target container for visiting.</param>
        /// <returns>A visitor to provide a result.</returns>
        IContainerVisitor<TResult> Visit(Container container);
    }
}