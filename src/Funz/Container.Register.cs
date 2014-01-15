using System;
using System.Diagnostics;

namespace Jwc.Funz
{
	public partial class Container
	{
		/* Contain just the typed overloads that are just pass-through to the real implementations.
		 * They all have DebuggerStepThrough to ease debugging. */

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2>(Func<Container, TArg1, TArg2, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2>(object key, Func<Container, TArg1, TArg2, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3>(Func<Container, TArg1, TArg2, TArg3, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3>(object key, Func<Container, TArg1, TArg2, TArg3, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4>(Func<Container, TArg1, TArg2, TArg3, TArg4, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(key, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(_noKey, factory);
		}

		/// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="key">
        /// A key used to differenciate this service registration.
        /// </param>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
		[DebuggerStepThrough]
		public IRegistration Register<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key, Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService> factory)
		{
			return RegisterImpl<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(key, factory);
		}

	}
}
