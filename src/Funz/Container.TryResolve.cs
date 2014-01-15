using System.Diagnostics;

namespace Jwc.Funz
{
    public partial class Container
    {
        /* Contain just the typed overloads that are just pass-through to the real implementations.
         * They all have DebuggerStepThrough to ease debugging. */

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        {
            return ResolveImpl<TService, TArg1, TArg2>(_noKey, false, arg1, arg2);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2>(object key, TArg1 arg1, TArg2 arg2)
        {
            return ResolveImpl<TService, TArg1, TArg2>(key, false, arg1, arg2);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3>(_noKey, false, arg1, arg2, arg3);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3>(key, false, arg1, arg2, arg3);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(_noKey, false, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(key, false, arg1, arg2, arg3, arg4);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(_noKey, false, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(key, false, arg1, arg2, arg3, arg4, arg5);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(key, false, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        /// <summary>
        /// Tries to resolve the given service by type, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(_noKey, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, with passing arguments for its construction.
        /// </summary>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        [DebuggerStepThrough]
        public TService TryResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            return ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(key, false, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

    }
}
