using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Jwc.Funz
{
    /// <summary>
    /// Main container class for components, supporting container hierarchies and
    /// lifetime management of <see cref="IDisposable"/> instances.
    /// </summary>
    public partial class Container : IDisposable
    {
        private static readonly object _noKey = new object();

        private readonly ICollection<Container> _children = new ContainerCollection();
        private readonly IDictionary<ServiceKey, Registration> _registry =
            new ConcurrentDictionary<ServiceKey, Registration>();
        private readonly Container _parent;
        private readonly object _scope;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container()
            : this(new object())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class with container scope.
        /// </summary>
        /// <param name="scope">
        /// The scope to represent custom lifetime.
        /// </param>
        public Container(object scope)
            : this(null, scope)
        {
        }

        private Container(Container parent, object scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _parent = parent;
            _scope = scope;

            Register(c => this).ReusedWithinContainer().OwnedByExternal();
        }

        /// <summary>
        /// Gets a value indicating the container scope.
        /// </summary>
        public object Scope
        {
            get
            {
                ThrowExceptionIfDisposed();

                return _scope;
            }
        }

        /// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <typeparam name="TService">
        /// The service type to register.
        /// </typeparam>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService>(Func<Container, TService> factory)
        {
            return RegisterImpl<Func<Container, TService>, TService>(_noKey, factory);
        }

        /// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <param name="factory">
        /// The factory delegate to initialize new instances of the service when needed.
        /// </param>
        /// <typeparam name="TService">
        /// The service type to register.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// First argument that should be passed to the factory delegate to create the instace.
        /// </typeparam>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            return RegisterImpl<Func<Container, TArg, TService>, TService>(_noKey, factory);
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
        /// <typeparam name="TService">
        /// The service type to register.
        /// </typeparam>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService>(object key, Func<Container, TService> factory)
        {
            return RegisterImpl<Func<Container, TService>, TService>(key, factory);
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
        /// <typeparam name="TService">
        /// The service type to register.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// First argument that should be passed to the factory delegate to create the instace.
        /// </typeparam>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService, TArg>(object key, Func<Container, TArg, TService> factory)
        {
            return RegisterImpl<Func<Container, TArg, TService>, TService>(key, factory);
        }

        /// <summary>
        /// Resolves the given service by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService Resolve<TService>()
        {
            return ResolveImpl<TService>(_noKey, true);
        }

        /// <summary>
        /// Resolves the given service by type, without passing any arguments for its construction.
        /// </summary>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instace.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService Resolve<TService, TArg>(TArg arg)
        {
            return ResolveImpl<TService, TArg>(_noKey, true, arg);
        }

        /// <summary>
        /// Resolves the given service by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService ResolveKeyed<TService>(object key)
        {
            return ResolveImpl<TService>(key, true);
        }

        /// <summary>
        /// Resolves the given service by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instace.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService ResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return ResolveImpl<TService, TArg>(key, true, arg);
        }

        /// <summary>
        /// Tries to resolve the given service by type, without passing any arguments for its construction.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolve<TService>()
        {
            return ResolveImpl<TService>(_noKey, false);
        }

        /// <summary>
        /// Tries to resolve the given service by type, without passing any arguments for its construction.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instace.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolve<TService, TArg>(TArg arg)
        {
            return ResolveImpl<TService, TArg>(_noKey, false, arg);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, without passing arguments for its initialization.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolveKeyed<TService>(object key)
        {
            return ResolveImpl<TService>(key, false);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, without passing arguments for its initialization.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instace.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return ResolveImpl<TService, TArg>(key, false, arg);
        }

        /// <summary>
        /// Resolves the given factory by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TService> LazyResolve<TService>()
        {
            return () => ResolveImpl<TService>(_noKey, true);
        }

        /// <summary>
        /// Resolves the given factory by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TArg, TService> LazyResolve<TService, TArg>()
        {
            return arg => ResolveImpl<TService, TArg>(_noKey, true, arg);
        }

        /// <summary>
        /// Resolves the given factory by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TService> LazyResolveKeyed<TService>(object key)
        {
            return () => ResolveImpl<TService>(key, true);
        }

        /// <summary>
        /// Resolves the given factory by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TArg, TService> LazyResolveKeyed<TService, TArg>(object key)
        {
            return arg => ResolveImpl<TService, TArg>(key, true, arg);
        }

        /// <summary>
        /// Creates a child container of the current one, which exposes its
        /// current service registration to the new child container.
        /// </summary>
        /// <returns>
        /// The new child container.
        /// </returns>
        public Container CreateChild()
        {
            return CreateChild(new object());
        }

        /// <summary>
        /// Creates a child container of the current one with custom scope, which exposes its
        /// current service registration to the new child container.
        /// </summary>
        /// <param name="scope">
        /// The scope to represent custom lifetime.
        /// </param>
        /// <returns>
        /// The new child container.
        /// </returns>
        public Container CreateChild(object scope)
        {
            ThrowExceptionIfDisposed();

            var container = new Container(this, scope);
            _children.Add(container);
            return container;
        }

        /// <summary>
        /// Disposes the container and all instances owned by it, as well as all child containers
        ///	created through <see cref="CreateChild()"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the container and all instances owned by it, as well as all child containers
        ///	created through <see cref="CreateChild()"/>.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether managed resources are included to be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (!disposing)
                return;

            DisposeServices();
            DisposeChildren();
            RemoveFromParent();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "The disposing process of the registration will be performed on a container.")]
        private IRegistration RegisterImpl<TFunc, TService>(object key, TFunc factory) where TFunc : class
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            ThrowExceptionIfDisposed();

            var registration = new Registration<TFunc, TService>(this, factory, new HierarchyScope());
            _registry[new ServiceKey(typeof(TFunc), key)] = registration;
            return registration;
        }

        private TService ResolveImpl<TService>(object key, bool throws)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TService>), key);
            var registration = GetRegistration<Func<Container, TService>, TService>(serviceKey, throws);
            if (registration == null)
                return default(TService);

            if (registration.HasService)
                return registration.Service;

            using (RecursionGuard.Inspect(serviceKey))
            {
                var service = registration.Factory.Invoke(this);
                registration.Service = service;
                return service;
            }
        }

        private TService ResolveImpl<TService, TArg>(object key, bool throws, TArg arg)
        {
            var serviceKey = new ServiceKey(typeof(Func<Container, TArg, TService>), key);
            var registration = GetRegistration<Func<Container, TArg, TService>, TService>(serviceKey, throws);
            if (registration == null)
                return default(TService);

            if (registration.HasService)
                return registration.Service;

            using (RecursionGuard.Inspect(serviceKey))
            {
                var service = registration.Factory.Invoke(this, arg);
                registration.Service = service;
                return service;
            }
        }

        private Registration<TFunc, TService> GetRegistration<TFunc, TService>(ServiceKey serviceKey, bool throws)
        {
            ThrowExceptionIfDisposed();

            Container current = this;
            Registration registration = null;
            while (current != null && !current._registry.TryGetValue(serviceKey, out registration))
                current = current._parent;

            if (registration != null)
                return registration.Clone<TFunc, TService>(this, serviceKey);

            if (throws)
                ThrowNotRegistered(serviceKey);

            return null;
        }

        private static Type[] GetArgumentTypes(Type factoryType)
        {
            var genericArguments = factoryType.GetGenericArguments();
            return genericArguments.Skip(1).Take(genericArguments.Length - 2).ToArray();
        }

        private void DisposeServices()
        {
            foreach (var registration in _registry.Values)
                registration.Dispose();
        }

        private void DisposeChildren()
        {
            foreach (var child in _children)
                child.Dispose();
        }

        private void RemoveFromParent()
        {
            if (_parent == null)
                return;

            _parent._children.Remove(this);
        }

        private void ThrowExceptionIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(ToString());
        }

        private static void ThrowNotRegistered(ServiceKey serviceKey)
        {
            var argumentTypes = GetArgumentTypes(serviceKey.FactoryType);
            var serviceType = serviceKey.FactoryType.GetGenericArguments().Last();

            if (serviceKey.Key == _noKey)
            {
                if (!argumentTypes.Any())
                {
                    throw new ResolutionException(string.Format(
                        CultureInfo.CurrentCulture,
                        "The service type '{0}' was not registered.",
                        serviceType));
                }

                throw new ResolutionException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The service type '{0}' with the argument(s) '{1}' was not registered.",
                    serviceType,
                    string.Join(", ", argumentTypes.Select(x => x.FullName))));
            }

            if (!argumentTypes.Any())
            {
                throw new ResolutionException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The service type '{0}' with the key '{1}' was not registered.",
                    serviceType,
                    serviceKey.Key));
            }

            throw new ResolutionException(string.Format(
                CultureInfo.CurrentCulture,
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was not registered.",
                serviceType,
                serviceKey.Key,
                string.Join(", ", argumentTypes.Select(x => x.FullName))));
        }

        private static void ThrowRecursionException(ServiceKey serviceKey)
        {
            var argumentTypes = GetArgumentTypes(serviceKey.FactoryType);
            var serviceType = serviceKey.FactoryType.GetGenericArguments().Last();

            if (serviceKey.Key == _noKey)
            {
                if (!argumentTypes.Any())
                {
                    throw new ResolutionException(string.Format(
                        CultureInfo.CurrentCulture,
                        "The service type '{0}' was registered recursively.",
                        serviceType));
                }

                throw new ResolutionException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The service type '{0}' with the argument(s) '{1}' was registered recursively.",
                    serviceType,
                    string.Join(", ", argumentTypes.Select(x => x.FullName))));
            }

            if (!argumentTypes.Any())
            {
                throw new ResolutionException(string.Format(
                    CultureInfo.CurrentCulture,
                    "The service type '{0}' with the key '{1}' was registered recursively.",
                    serviceType,
                    serviceKey.Key));
            }

            throw new ResolutionException(string.Format(
                CultureInfo.CurrentCulture,
                "The service type '{0}' with the key '{1}' and the argument(s) '{2}' was registered recursively.",
                serviceType,
                serviceKey.Key,
                string.Join(", ", argumentTypes.Select(x => x.FullName))));
        }

        private class ContainerCollection : Collection<Container>, IEnumerable<Container>
        {
            private readonly object _syncRoot;

            public ContainerCollection()
            {
                _syncRoot = ((ICollection)this).SyncRoot;
            }

            protected override void InsertItem(int index, Container item)
            {
                lock (_syncRoot)
                    base.InsertItem(index, item);
            }

            protected override void RemoveItem(int index)
            {
                lock (_syncRoot)
                    base.RemoveItem(index);
            }

            IEnumerator<Container> IEnumerable<Container>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private new IEnumerator<Container> GetEnumerator()
            {
                lock (_syncRoot)
                    return ((IEnumerable<Container>)this.ToArray()).GetEnumerator();
            }
        }

        private static class RecursionGuard
        {
            [ThreadStatic]
            private static Stack<ServiceKey> _serviceKeys;

            public static IDisposable Inspect(ServiceKey serviceKey)
            {
                if (_serviceKeys == null)
                    _serviceKeys = new Stack<ServiceKey>();

                if (_serviceKeys.Contains(serviceKey))
                    ThrowRecursionException(serviceKey);

                _serviceKeys.Push(serviceKey);
                return new EndInspection();
            }

            private class EndInspection : IDisposable
            {
                public void Dispose()
                {
                    _serviceKeys.Pop();
                }
            }
        }

        private sealed class ServiceKey
        {
            private readonly Type _factoryType;
            private readonly object _key;

            public ServiceKey(Type factoryType, object key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                _factoryType = factoryType;
                _key = key;
            }

            public Type FactoryType
            {
                get
                {
                    return _factoryType;
                }
            }

            public object Key
            {
                get
                {
                    return _key;
                }
            }

            private bool Equals(ServiceKey other)
            {
                return FactoryType == other.FactoryType && Key.Equals(other.Key);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;

                if (ReferenceEquals(this, obj))
                    return true;

                if (obj.GetType() != GetType())
                    return false;

                return Equals((ServiceKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (FactoryType.GetHashCode() * 397) ^ Key.GetHashCode();
                }
            }
        }

        private abstract class Registration : IRegistration, IDisposable
        {
            private ReusedScope _reusedScope;
            private bool _canDispose;

            protected Registration(ReusedScope reusedScope, bool canDispose)
            {
                _reusedScope = reusedScope;
                _canDispose = canDispose;
            }

            protected ReusedScope ReusedScope
            {
                get
                {
                    return _reusedScope;
                }
            }

            public bool CanDispose
            {
                get
                {
                    return _canDispose;
                }
            }

            public IOwned ReusedWithinNone()
            {
                _reusedScope = new NoneScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithinContainer()
            {
                _reusedScope = new ContainerScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithinHierarchy()
            {
                _reusedScope = new HierarchyScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithin(object scope)
            {
                _reusedScope = new CustomScope(scope) { Registration = this };
                return this;
            }

            public void OwnedByContainer()
            {
                _canDispose = true;
            }

            public void OwnedByExternal()
            {
                _canDispose = false;
            }

            public Registration<TFunc, TService> Clone<TFunc, TService>(Container container, ServiceKey serviceKey)
            {
                return ReusedScope.Clone<TFunc, TService>(container, serviceKey);
            }

            public void Dispose()
            {
                Dispose(true);
            }

            protected abstract void Dispose(bool disposing);
        }

        private sealed class Registration<TFunc, TService> : Registration
        {
            private readonly TFunc _factory;
            private readonly Container _container;
            private TService _service;
            private bool _hasService;

            public Registration(
                Container container,
                TFunc factory,
                ReusedScope reusedScope,
                bool canDispose = true)
                : base(reusedScope, canDispose)
            {
                _factory = factory;
                _container = container;

                reusedScope.Registration = this;
            }

            public TFunc Factory
            {
                get
                {
                    return _factory;
                }
            }

            public TService Service
            {
                get
                {
                    return _service;
                }
                set
                {
                    if (!ReusedScope.CanSave)
                        return;

                    _service = value;
                    _hasService = true;
                }
            }

            public bool HasService
            {
                get
                {
                    return _hasService;
                }
            }

            public Container Container
            {
                get
                {
                    return _container;
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (!disposing)
                    return;

                if (!CanDispose)
                    return;

                var disposable = Service as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        private abstract class ReusedScope
        {
            public abstract bool CanSave
            {
                get;
            }

            public Registration Registration
            {
                protected get;
                set;
            }

            public abstract Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey);
        }

        private class NoneScope : ReusedScope
        {
            public override bool CanSave
            {
                get
                {
                    return false;
                }
            }

            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                return (Registration<TFunc, TService>)Registration;
            }
        }

        private class ContainerScope : ReusedScope
        {
            public override bool CanSave
            {
                get
                {
                    return true;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Reliability",
                "CA2000:Dispose objects before losing scope",
                Justification = "The disposing process of the registration will be performed on a container.")]
            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                var registration = (Registration<TFunc, TService>)Registration;

                if (registration.Container == container)
                    return registration;

                var clone = new Registration<TFunc, TService>(
                    container,
                    registration.Factory,
                    new ContainerScope(),
                    registration.CanDispose);

                container._registry[serviceKey] = clone;
                return clone;
            }
        }

        private class HierarchyScope : ReusedScope
        {
            public override bool CanSave
            {
                get
                {
                    return true;
                }
            }

            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                return (Registration<TFunc, TService>)Registration;
            }
        }

        private class CustomScope : ReusedScope
        {
            private readonly object _scope;
            private bool _canSave;

            public CustomScope(object scope, bool canSave = false)
            {
                _scope = scope;
                _canSave = canSave;
            }

            public override bool CanSave
            {
                get
                {
                    return _canSave;
                }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Reliability",
                "CA2000:Dispose objects before losing scope",
                Justification = "The disposing process of the registration will be performed on a container.")]
            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                var registration = (Registration<TFunc, TService>)Registration;

                var scoped = GetScopedContainer(container);
                if (scoped == null)
                    return registration;

                _canSave = true;

                if (registration.Container == scoped)
                    return registration;

                var clone = new Registration<TFunc, TService>(
                    scoped,
                    registration.Factory,
                    new CustomScope(_scope, _canSave),
                    registration.CanDispose);

                scoped._registry[serviceKey] = clone;
                return clone;
            }

            private Container GetScopedContainer(Container container)
            {
                Container current = container;
                while (current != null && current.Scope != _scope)
                    current = current._parent;

                return current;
            }
        }
    }
}