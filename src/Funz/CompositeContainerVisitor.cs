using System;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Funz
{
    public class CompositeContainerVisitor<TResult> : IContainerVisitor<IEnumerable<TResult>>
    {
        private readonly IContainerVisitor<TResult>[] _visitors;

        public CompositeContainerVisitor(params IContainerVisitor<TResult>[] visitors)
        {
            if (visitors == null)
                throw new ArgumentNullException("visitors");
            
            _visitors = visitors;
        }

        public IEnumerable<TResult> Result
        {
            get
            {
                return _visitors.Select(v => v.Result);
            }
        }

        public IEnumerable<IContainerVisitor<TResult>> Visitors
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

            var newVisitors = Visitors.Select(v => v.Visit(container)).ToArray();
            return new CompositeContainerVisitor<TResult>(newVisitors);
        }
    }
}