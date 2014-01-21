using System;
using System.Diagnostics;

namespace Jwc.Funz
{
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
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(serviceKey, false) != null;
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolve<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>()
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>), _noKey);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(serviceKey, false) != null;
        }

		/// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1004:GenericMethodsShouldProvideTypeParameter")]
		[DebuggerStepThrough]
        public bool CanResolveKeyed<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>), key);
            return GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(serviceKey, false) != null;
        }

    }
}
