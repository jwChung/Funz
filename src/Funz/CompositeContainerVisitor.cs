﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Jwc.Funz
{
    /// <summary>
    /// Represents a composite container visitor.
    /// </summary>
    /// <typeparam name="TResult">The type of a enumerable result.</typeparam>
    public class CompositeContainerVisitor<TResult> : IContainerVisitor<IEnumerable<TResult>>
    {
        private readonly IContainerVisitor<TResult>[] visitors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeContainerVisitor{TResult}" /> class
        /// with visitors to compose.
        /// </summary>
        /// <param name="visitors">The visitors to compose.</param>
        public CompositeContainerVisitor(params IContainerVisitor<TResult>[] visitors)
        {
            if (visitors == null)
                throw new ArgumentNullException("visitors");

            this.visitors = visitors;
        }

        /// <summary>
        /// Gets a value to indicating the enumerable result produced after visiting.
        /// </summary>
        public IEnumerable<TResult> Result
        {
            get { return this.visitors.Select(v => v.Result); }
        }

        /// <summary>
        /// Gets a value indicating the visitors composed.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Suppressing this warning is desirable.")]
        public IEnumerable<IContainerVisitor<TResult>> Visitors
        {
            get { return this.visitors; }
        }

        /// <summary>
        /// Visits a container, which lets each visitor of composed visitors visit the container.
        /// </summary>
        /// <param name="container">A target container for visiting.</param>
        /// <returns>A composite container visitor to provide a enumerable result.</returns>
        public IContainerVisitor<IEnumerable<TResult>> Visit(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var newVisitors = this.Visitors.Select(v => v.Visit(container)).ToArray();
            return new CompositeContainerVisitor<TResult>(newVisitors);
        }
    }
}