using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Jwc.Funz
{
    /// <summary>
    /// Main container class for components, supporting container hierarchies and
    /// lifetime management of <see cref="IDisposable" /> instances.
    /// </summary>
    public partial class Container : IDisposable
    {
        private static readonly object NoKey = new object();

        private readonly ContainerCollection children = new ContainerCollection();

        private readonly IDictionary<ServiceKey, Registration> registry =
            new ConcurrentDictionary<ServiceKey, Registration>();

        private readonly Container parent;
        private readonly object scope;
        private volatile bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container" /> class.
        /// </summary>
        public Container() : this(CreateScope())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container" /> class with container scope.
        /// </summary>
        /// <param name="scope">The scope to represent custom lifetime.</param>
        public Container(object scope)
            : this(null, scope)
        {
        }

        private Container(Container parent, object scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            this.parent = parent;
            this.scope = scope;

            Register(c => this).ReusedWithinContainer().OwnedByExternal();
        }

        /// <summary>
        /// Gets a value indicating the container scope.
        /// </summary>
        /// <value>
        /// The scope.
        /// </value>
        public object Scope
        {
            get
            {
                this.ThrowExceptionIfDisposed();

                return this.scope;
            }
        }

        /// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="factory">The factory delegate to initialize new instances of the service when needed.</param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService>(Func<Container, TService> factory)
        {
            return RegisterImpl<Func<Container, TService>, TService>(NoKey, factory);
        }

        /// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TArg">First argument that should be passed to the factory delegate to create the instance.</typeparam>
        /// <param name="factory">The factory delegate to initialize new instances of the service when needed.</param>
        /// <returns>
        /// The registration object to perform further configuration via its fluent interface.
        /// </returns>
        public IRegistration Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            return RegisterImpl<Func<Container, TArg, TService>, TService>(NoKey, factory);
        }

        /// <summary>
        /// Registers the given service by providing a factory delegate to instantiate it.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <param name="key">A key used to differenciate this service registration.</param>
        /// <param name="factory">The factory delegate to initialize new instances of the service when needed.</param>
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
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TArg">First argument that should be passed to the factory delegate to create the instance.</typeparam>
        /// <param name="key">A key used to differenciate this service registration.</param>
        /// <param name="factory">The factory delegate to initialize new instances of the service when needed.</param>
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
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService Resolve<TService>()
        {
            return this.ResolveImpl<TService>(NoKey, true);
        }

        /// <summary>
        /// Resolves the given service by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="arg">The first argument to pass to the factory delegate that may create the instance.</param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService Resolve<TService, TArg>(TArg arg)
        {
            return this.ResolveImpl<TService, TArg>(NoKey, true, arg);
        }

        /// <summary>
        /// Resolves the given service by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService ResolveKeyed<TService>(object key)
        {
            return this.ResolveImpl<TService>(key, true);
        }

        /// <summary>
        /// Resolves the given service by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <param name="arg">The first argument to pass to the factory delegate that may create the instance.</param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService ResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return this.ResolveImpl<TService, TArg>(key, true, arg);
        }

        /// <summary>
        /// Tries to resolve the given service by type, without passing any arguments for its construction.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolve<TService>()
        {
            return this.ResolveImpl<TService>(NoKey, false);
        }

        /// <summary>
        /// Tries to resolve the given service by type, without passing any arguments for its construction.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="arg">The first argument to pass to the factory delegate that may create the instance.</param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolve<TService, TArg>(TArg arg)
        {
            return this.ResolveImpl<TService, TArg>(NoKey, false, arg);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, without passing arguments for its initialization.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolveKeyed<TService>(object key)
        {
            return this.ResolveImpl<TService>(key, false);
        }

        /// <summary>
        /// Tries to resolve the given service by type and key, without passing arguments for its initialization.
        /// If the service is not registered, it will return a default value of the type.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <param name="arg">
        /// The first argument to pass to the factory delegate that may create the instance.
        /// </param>
        /// <returns>
        /// The resolved service instance.
        /// </returns>
        public TService TryResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return this.ResolveImpl<TService, TArg>(key, false, arg);
        }

        /// <summary>
        /// Resolves the given factory by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TService> LazyResolve<TService>()
        {
            return () => this.ResolveImpl<TService>(NoKey, true);
        }

        /// <summary>
        /// Resolves the given factory by type, without passing any arguments for its construction.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TArg, TService> LazyResolve<TService, TArg>()
        {
            return arg => this.ResolveImpl<TService, TArg>(NoKey, true, arg);
        }

        /// <summary>
        /// Resolves the given factory by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TService> LazyResolveKeyed<TService>(object key)
        {
            return () => this.ResolveImpl<TService>(key, true);
        }

        /// <summary>
        /// Resolves the given factory by type and key, without passing arguments for its initialization.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The resolved service factory.
        /// </returns>
        public Func<TArg, TService> LazyResolveKeyed<TService, TArg>(object key)
        {
            return arg => this.ResolveImpl<TService, TArg>(key, true, arg);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        public bool CanResolve<TService>()
        {
            return CanResolveImpl<Func<Container, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type or not.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        public bool CanResolve<TService, TArg>()
        {
            return CanResolveImpl<Func<Container, TArg, TService>, TService>(NoKey);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        public bool CanResolveKeyed<TService>(object key)
        {
            return CanResolveImpl<Func<Container, TService>, TService>(key);
        }

        /// <summary>
        /// Determines whether this container can resolve a service of the type with the key or not.
        /// </summary>
        /// <typeparam name="TService">The type of the service to retrieve.</typeparam>
        /// <typeparam name="TArg">The type of the first argument.</typeparam>
        /// <param name="key">The key of the service to retrieve.</param>
        /// <returns>
        /// The result whether this container can resolve.
        /// </returns>
        public bool CanResolveKeyed<TService, TArg>(object key)
        {
            return CanResolveImpl<Func<Container, TArg, TService>, TService>(key);
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
            return this.CreateChild(CreateScope());
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
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The disposing process of the container will be performed explicitly.")]
        public Container CreateChild(object scope)
        {
            this.ThrowExceptionIfDisposed();

            var container = new Container(this, scope);
            this.children.Add(container);
            return container;
        }

        /// <summary>
        /// Disposes the container and all instances owned by it, as well as all child containers
        /// created through <see cref="CreateChild()" />.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Accepts a container visitor.
        /// </summary>
        /// <param name="visitor">A container visitor.</param>
        /// <typeparam name="TResult">A result type.</typeparam>
        /// <returns>A visitor to provide a result produced after visiting.</returns>
        public IContainerVisitor<TResult> Accept<TResult>(IContainerVisitor<TResult> visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");

            this.ThrowExceptionIfDisposed();

            return visitor.Visit(this);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Scope.ToString();
        }

        /// <summary>
        /// Disposes the container and all instances owned by it, as well as all child containers
        /// created through <see cref="CreateChild()" />.
        /// </summary>
        /// <param name="disposing">
        /// Indicates whether managed resources are included to be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            this.disposed = true;

            if (!disposing)
                return;

            this.DisposeServices();
            this.DisposeChildren();
            this.RemoveFromParent();
        }

        private static Type[] GetArgumentTypes(Type factoryType)
        {
            var genericArguments = factoryType.GetGenericArguments();
            return genericArguments.Skip(1).Take(genericArguments.Length - 2).ToArray();
        }

        private static void ThrowNotRegistered(ServiceKey serviceKey)
        {
            var argumentTypes = GetArgumentTypes(serviceKey.FactoryType);
            var serviceType = serviceKey.FactoryType.GetGenericArguments().Last();

            if (serviceKey.Key == NoKey)
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

            if (serviceKey.Key == NoKey)
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

        private static string CreateScope()
        {
            return "Container" + Guid.NewGuid().ToString("N");
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The disposing process of the registration will be performed on a container.")]
        private IRegistration RegisterImpl<TFunc, TService>(object key, TFunc factory) where TFunc : class
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.ThrowExceptionIfDisposed();

            var registration = new Registration<TFunc, TService>(this, factory, new HierarchyScope());
            this.registry[new ServiceKey(typeof(TFunc), key)] = registration;
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

        private bool CanResolveImpl<TFunc, TService>(object key)
        {
            return this.GetRegistration<TFunc, TService>(new ServiceKey(typeof(TFunc), key), false) != null;
        }

        private Registration<TFunc, TService> GetRegistration<TFunc, TService>(ServiceKey serviceKey, bool throws)
        {
            this.ThrowExceptionIfDisposed();

            Container current = this;
            Registration registration = null;
            while (current != null && !current.registry.TryGetValue(serviceKey, out registration))
                current = current.parent;

            if (registration != null)
                return registration.Clone<TFunc, TService>(this, serviceKey);

            if (throws)
                ThrowNotRegistered(serviceKey);

            return null;
        }

        private void DisposeServices()
        {
            foreach (var registration in this.registry.Values)
                registration.Dispose();
        }

        private void DisposeChildren()
        {
            foreach (var child in this.children)
                child.Dispose();
        }

        private void RemoveFromParent()
        {
            if (this.parent == null)
                return;

            this.parent.children.Remove(this);
        }

        private void ThrowExceptionIfDisposed()
        {
            if (this.disposed)
                throw new ObjectDisposedException("Jwc.Funz.Container");
        }

        private static class RecursionGuard
        {
            [ThreadStatic]
            private static Stack<ServiceKey> serviceKeys;

            public static IDisposable Inspect(ServiceKey serviceKey)
            {
                if (serviceKeys == null)
                    serviceKeys = new Stack<ServiceKey>();

                if (serviceKeys.Contains(serviceKey))
                    ThrowRecursionException(serviceKey);

                serviceKeys.Push(serviceKey);
                return new EndInspection();
            }

            private class EndInspection : IDisposable
            {
                public void Dispose()
                {
                    serviceKeys.Pop();
                }
            }
        }

        private class ContainerCollection : IEnumerable<Container>
        {
            private readonly IList<Container> containers = new List<Container>();

            public void Add(Container container)
            {
                lock (this.containers)
                    this.containers.Add(container);
            }

            public void Remove(Container container)
            {
                lock (this.containers)
                    this.containers.Remove(container);
            }

            public IEnumerator<Container> GetEnumerator()
            {
                lock (this.containers)
                    return ((IEnumerable<Container>)this.containers.ToArray()).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        private sealed class ServiceKey
        {
            private readonly Type factoryType;
            private readonly object key;

            public ServiceKey(Type factoryType, object key)
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                this.factoryType = factoryType;
                this.key = key;
            }

            public Type FactoryType
            {
                get { return this.factoryType; }
            }

            public object Key
            {
                get { return this.key; }
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                    return false;

                if (ReferenceEquals(this, obj))
                    return true;

                if (obj.GetType() != GetType())
                    return false;

                return this.Equals((ServiceKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (this.FactoryType.GetHashCode() * 397) ^ this.Key.GetHashCode();
                }
            }

            private bool Equals(ServiceKey other)
            {
                return this.FactoryType == other.FactoryType && this.Key.Equals(other.Key);
            }
        }

        private abstract class Registration : IRegistration, IDisposable
        {
            private ReusedScope reusedScope;
            private bool canDispose;

            protected Registration(ReusedScope reusedScope, bool canDispose)
            {
                this.reusedScope = reusedScope;
                this.canDispose = canDispose;
            }

            public bool CanDispose
            {
                get { return this.canDispose; }
            }

            protected ReusedScope ReusedScope
            {
                get { return this.reusedScope; }
            }

            public IOwned ReusedWithinNone()
            {
                this.reusedScope = new NoneScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithinContainer()
            {
                this.reusedScope = new ContainerScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithinHierarchy()
            {
                this.reusedScope = new HierarchyScope { Registration = this };
                return this;
            }

            public IOwned ReusedWithin(object scope)
            {
                this.reusedScope = new CustomScope(scope) { Registration = this };
                return this;
            }

            public void OwnedByContainer()
            {
                this.canDispose = true;
            }

            public void OwnedByExternal()
            {
                this.canDispose = false;
            }

            public Registration<TFunc, TService> Clone<TFunc, TService>(Container container, ServiceKey serviceKey)
            {
                return ReusedScope.Clone<TFunc, TService>(container, serviceKey);
            }

            public void Dispose()
            {
                this.Dispose(true);
            }

            protected abstract void Dispose(bool disposing);
        }

        private sealed class Registration<TFunc, TService> : Registration
        {
            private readonly TFunc factory;
            private readonly Container container;
            private TService service;
            private bool hasService;

            public Registration(
                Container container,
                TFunc factory,
                ReusedScope reusedScope,
                bool canDispose = true)
                : base(reusedScope, canDispose)
            {
                this.factory = factory;
                this.container = container;

                reusedScope.Registration = this;
            }

            public TFunc Factory
            {
                get { return this.factory; }
            }

            public TService Service
            {
                get
                {
                    return this.service;
                }

                set
                {
                    if (!ReusedScope.CanSave)
                        return;

                    this.service = value;
                    this.hasService = true;
                }
            }

            public bool HasService
            {
                get { return this.hasService; }
            }

            public Container Container
            {
                get { return this.container; }
            }

            protected override void Dispose(bool disposing)
            {
                if (!disposing)
                    return;

                if (!this.CanDispose)
                    return;

                var disposable = this.Service as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
        }

        private abstract class ReusedScope
        {
            public abstract bool CanSave { get; }

            public Registration Registration { protected get; set; }

            public abstract Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey);
        }

        private class NoneScope : ReusedScope
        {
            public override bool CanSave
            {
                get { return false; }
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
                get { return true; }
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

                container.registry[serviceKey] = clone;
                return clone;
            }
        }

        private class HierarchyScope : ReusedScope
        {
            public override bool CanSave
            {
                get { return true; }
            }

            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                return (Registration<TFunc, TService>)Registration;
            }
        }

        private class CustomScope : ReusedScope
        {
            private readonly object scope;
            private bool canSave;

            public CustomScope(object scope, bool canSave = false)
            {
                this.scope = scope;
                this.canSave = canSave;
            }

            public override bool CanSave
            {
                get { return this.canSave; }
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage(
                "Microsoft.Reliability",
                "CA2000:Dispose objects before losing scope",
                Justification = "The disposing process of the registration will be performed on a container.")]
            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                var registration = (Registration<TFunc, TService>)Registration;

                var scoped = this.GetScopedContainer(container);
                if (scoped == null)
                    return registration;

                this.canSave = true;

                if (registration.Container == scoped)
                    return registration;

                var clone = new Registration<TFunc, TService>(
                    scoped,
                    registration.Factory,
                    new CustomScope(this.scope, this.canSave),
                    registration.CanDispose);

                scoped.registry[serviceKey] = clone;
                return clone;
            }

            private Container GetScopedContainer(Container container)
            {
                Container current = container;
                while (current != null && current.Scope != this.scope)
                    current = current.parent;

                return current;
            }
        }
    }
}