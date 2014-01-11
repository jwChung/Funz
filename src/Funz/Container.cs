using System;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Funz
{
    /// <summary>
    /// Main container class for components, supporting container hierarchies and
    /// lifetime management of <see cref="IDisposable"/> instances.
    /// </summary>
    public class Container : IDisposable
    {
        private static readonly object _noKey = new object();

        private readonly Container _parent;
        private readonly ICollection<Container> _children;
        private readonly object _scope;
        private readonly IDictionary<ServiceKey, Registration> _registry;

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
        public Container(object scope)
            : this(null, scope)
        {
        }

        private Container(Container parent, object scope)
        {
            if (scope == null)
            {
                throw new ArgumentNullException("scope");
            }
            
            _parent = parent;
            _scope = scope;

            _children = new List<Container>();
            _registry = new Dictionary<ServiceKey, Registration>();
            _disposed = false;
        }

        /// <summary>
        /// Gets a value indicating the container scope.
        /// </summary>
        public object Scope
        {
            get
            {
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
            return ResolveImpl<TService>();
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
            return GetRegistration<Func<Container, TArg, TService>, TService>(_noKey).Factory.Invoke(this, arg);
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
            return GetRegistration<Func<Container, TService>, TService>(key).Factory.Invoke(this);
        }

        /// <summary>
        /// Resolves the given service by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <typeparam name="TService">
        /// Type of the service to retrieve.
        /// </typeparam>
        /// <typeparam name="TArg">
        /// Type of the first argument.
        /// </typeparam>
        /// <param name="key">
        /// The key of the service to retrieve.
        /// </param>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instace.
        /// </param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService ResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return GetRegistration<Func<Container, TArg, TService>, TService>(key).Factory.Invoke(this, arg);
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
            {
                return;
            }

            if (!disposing)
            {
                return;
            }

            DisposeServices();
            DisposeChildren();
            RemoveFromParent();

            _disposed = true;
        }

        private void DisposeServices()
        {
            foreach (var registration in _registry.Values)
            {
                registration.Dispose();
            }
        }

        private void DisposeChildren()
        {
            foreach (var child in _children.ToArray())
            {
                child.Dispose();
            }
        }

        private void RemoveFromParent()
        {
            if (_parent != null)
            {
                _parent._children.Remove(this);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "The disposing process of the registration will be performed on a container.")]
        private IRegistration RegisterImpl<TFunc, TService>(object key, TFunc factory) where TFunc : class
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            var registration = new Registration<TFunc, TService>(this, factory, new HierarchyScope());
            _registry[new ServiceKey(typeof(TFunc), key)] = registration;
            return registration;
        }

        private TService ResolveImpl<TService>()
        {
            var registration = GetRegistration<Func<Container, TService>, TService>(_noKey);
            if (registration.HasService)
            {
                return registration.Service;
            }

            var service = registration.Factory.Invoke(this);
            registration.Service = service;
            return service;
        }

        private Registration<TFunc, TService> GetRegistration<TFunc, TService>(object key)
        {
            var serviceKey = new ServiceKey(typeof(TFunc), key);

            Container current = this;
            Registration registration = null;
            while (current != null && !current._registry.TryGetValue(serviceKey, out registration))
            {
                current = current._parent;
            }

            if (registration != null)
            {
                return registration.Clone<TFunc, TService>(this, serviceKey);
            }

            var argumentTypes = GetArgumentTypes(typeof(TFunc));

            if (key == _noKey)
            {
                throw new ResolutionException(typeof(TService), argumentTypes);
            }

            throw new ResolutionException(typeof(TService), serviceKey.Key, argumentTypes);
        }

        private static Type[] GetArgumentTypes(Type factoryType)
        {
            var genericArguments = factoryType.GetGenericArguments();
            return genericArguments.Skip(1).Take(genericArguments.Length - 2).ToArray();
        }

        private sealed class ServiceKey
        {
            private readonly Type _factoryType;
            private readonly object _key;

            public ServiceKey(Type factoryType, object key)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                _factoryType = factoryType;
                _key = key;
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
                return _factoryType == other._factoryType && Key.Equals(other.Key);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }
                return Equals((ServiceKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (_factoryType.GetHashCode() * 397) ^ Key.GetHashCode();
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
                _hasService = false;

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
                    {
                        return;
                    }

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
                {
                    return;
                }

                if (!CanDispose)
                {
                    return;
                }

                var disposable = Service as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
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
                {
                    return registration;
                }

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
                {
                    return registration;
                }

                _canSave = true;

                if (registration.Container == scoped)
                {
                    return registration;
                }

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
                Container scoped = null;
                Container current = container;
                while (current != null)
                {
                    if (current.Scope == _scope)
                    {
                        scoped = current;
                        break;
                    }

                    current = current._parent;
                }

                return scoped;
            }
        }
    }
}