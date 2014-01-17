using System;
using System.Diagnostics;

namespace Jwc.Funz
{
    public partial class Container
    {
        /* Contain just the typed overloads that are just pass-through to the real implementations.
         * They all have DebuggerStepThrough to ease debugging. */

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TService> LazyResolve<TService, TArg1, TArg2>()
        {
            return (arg1, arg2) => ResolveImpl<TService, TArg1, TArg2>(_noKey, true, arg1, arg2);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TService> LazyResolveKeyed<TService, TArg1, TArg2>(object key)
        {
            return (arg1, arg2) => ResolveImpl<TService, TArg1, TArg2>(key, true, arg1, arg2);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TService> LazyResolve<TService, TArg1, TArg2, TArg3>()
        {
            return (arg1, arg2, arg3) => ResolveImpl<TService, TArg1, TArg2, TArg3>(_noKey, true, arg1, arg2, arg3);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3>(object key)
        {
            return (arg1, arg2, arg3) => ResolveImpl<TService, TArg1, TArg2, TArg3>(key, true, arg1, arg2, arg3);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4>()
        {
            return (arg1, arg2, arg3, arg4) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(_noKey, true, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4>(object key)
        {
            return (arg1, arg2, arg3, arg4) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(key, true, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5>()
        {
            return (arg1, arg2, arg3, arg4, arg5) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(_noKey, true, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(key, true, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(key, true, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Resolves the given factory by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService> LazyResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>()
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(_noKey, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// Resolves the given factory by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        [DebuggerStepThrough]
        public Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService> LazyResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key)
        {
            return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) => ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(key, true, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

    }
}
