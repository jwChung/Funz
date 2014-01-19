using System.Collections.Generic;

namespace Jwc.Funz
{
    public class CompositContainerVisitor<TResult> : IContainerVisitor<IEnumerable<TResult>>
    {
        public IEnumerable<TResult> Result
        {
            get;
            private set;
        }

        public IContainerVisitor<IEnumerable<TResult>> Visit(Container container)
        {
            throw new System.NotImplementedException();
        }
    }
}