using System;
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
            if (container == null)
                throw new ArgumentNullException("container");

            throw new System.NotImplementedException();
        }
    }
}