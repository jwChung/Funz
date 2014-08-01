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
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            return this.ResolveImpl<TService, TArg1, TArg2>(NoKey, true, arg1, arg2);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2>(object key, TArg1 arg1, TArg2 arg2)
        {
            return this.ResolveImpl<TService, TArg1, TArg2>(key, true, arg1, arg2);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3>(NoKey, true, arg1, arg2, arg3);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3>(key, true, arg1, arg2, arg3);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(NoKey, true, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(key, true, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(NoKey, true, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(key, true, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(key, true, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Resolves the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService Resolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(NoKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// Resolves the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService ResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            return this.ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

    }
}
