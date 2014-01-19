namespace Jwc.Funz
{
    public interface IContainerVisitor<TResult>
    {
        TResult Result { get; }

        IContainerVisitor<TResult> Visit(Container container);
    }
}