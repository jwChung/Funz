using System;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Funz
{
    public class CompositContainerVisitor<TResult> : IContainerVisitor<IEnumerable<TResult>>
    {
        private readonly IContainerVisitor<TResult>[] _visitors;
        private readonly IEnumerable<TResult> _result;

        public CompositContainerVisitor(params IContainerVisitor<TResult>[] visitors)
        {
            if (visitors == null)
                throw new ArgumentNullException("visitors");

            _visitors = visitors;
        }

        private CompositContainerVisitor(IEnumerable<TResult> result)
        {
            _result = result;
        }

        public IEnumerable<TResult> Result
        {
            get
            {
                return _result;
            }
        }

        public IContainerVisitor<TResult>[] Visitors
        {
            get
            {
                return _visitors;
            }
        }

        public IContainerVisitor<IEnumerable<TResult>> Visit(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var result = Visitors.Select(v => v.Visit(container).Result);
            return new CompositContainerVisitor<TResult>(result);
        }
    }
}