namespace Jwc.Funz
{
    public interface IContainerVisitor<out TResult>
    {
        TResult Result { get; }

        IContainerVisitor<TResult> Visit(Container container);
    }
}