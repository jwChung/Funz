using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Jwc.Funz
{
    /// <summary>
    /// Main container class for components, supporting container hierarchies and
    /// lifetime management of <see cref="IDisposable" /> instances.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1508:ClosingCurlyBracketsMustNotBePrecededByBlankLine", Justification = "The last line is automatically generated.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1618:GenericTypeParametersMustBeDocumented", Justification = "Suppressing this rule is desirable.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Suppressing this rule is desirable.")]
    public partial class Container
    {
        /* Contain just the typed overloads that are just pass-through to the real implementations.
         * They all have DebuggerStepThrough to ease debugging. */

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>()
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The generic types are to provide information of explicit types.")]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key)
        {
            return CanResolveImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(key);
        }

    }
}
