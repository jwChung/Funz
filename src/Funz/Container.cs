using System;
using System.Collections.Generic;

namespace Jwc.Funz
{
    public class Container
    {
        private readonly Container _parent;
        private readonly object _scope;
        private static readonly object _noKey = new object();
        private readonly IDictionary<ServiceKey, Registration> _registry;

        public Container()
            : this(new object())
        {
        }

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

            _registry = new Dictionary<ServiceKey, Registration>();
            _parent = parent;
            _scope = scope;
        }

        public object Scope
        {
            get
            {
                return _scope;
            }
        }

        internal Container Parent
        {
            get
            {
                return _parent;
            }
        }

        public IRegistration Register<TService>(Func<Container, TService> factory)
        {
            return RegisterImpl<Func<Container, TService>, TService>(_noKey, factory);
        }

        public void Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            RegisterImpl<Func<Container, TArg, TService>, TService>(_noKey, factory);
        }

        public void Register<TService>(object key, Func<Container, TService> factory)
        {
            RegisterImpl<Func<Container, TService>, TService>(key, factory);
        }

        public void Register<TService, TArg>(object key, Func<Container, TArg, TService> factory)
        {
            RegisterImpl<Func<Container, TArg, TService>, TService>(key, factory);
        }

        public TService Resolve<TService>()
        {
            return ResolveImpl<TService>();
        }

        public TService Resolve<TService, TArg>(TArg arg)
        {
            return GetRegistration<Func<Container, TArg, TService>, TService>(_noKey).Factory.Invoke(this, arg);
        }

        public TService ResolveKeyed<TService>(object key)
        {
            return GetRegistration<Func<Container, TService>, TService>(key).Factory.Invoke(this);
        }

        public TService ResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return GetRegistration<Func<Container, TArg, TService>, TService>(key).Factory.Invoke(this, arg);
        }

        public Container CreateChild()
        {
            return CreateChild(new object());
        }

        public Container CreateChild(object scope)
        {
            return new Container(this, scope);
        }

        private IRegistration RegisterImpl<TFunc, TService>(object key, TFunc factory) where TFunc : class
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            var registration = new Registration<TFunc, TService>(this, factory, new NoneScope());
            _registry[new ServiceKey(typeof(TService), key)] = registration;
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
            var serviceKey = new ServiceKey(typeof(TService), key);

            Container current = this;
            Registration registration = null;
            while (current != null && !current._registry.TryGetValue(serviceKey, out registration))
            {
                current = current.Parent;
            }

            if (registration == null)
            {
                throw new ResolutionException(typeof(TService), serviceKey.Key);
            }

            return registration.Clone<TFunc, TService>(this, serviceKey);
        }

        private sealed class ServiceKey
        {
            private readonly Type _servideType;
            private readonly object _key;

            public ServiceKey(Type servideType, object key)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }

                _servideType = servideType;
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
                return _servideType == other._servideType && Key.Equals(other.Key);
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
                    return (_servideType.GetHashCode() * 397) ^ Key.GetHashCode();
                }
            }
        }

        private abstract class Registration : IRegistration
        {
            private ReusedScope _reusedScope;

            protected Registration(ReusedScope reusedScope)
            {
                _reusedScope = reusedScope;
            }

            protected ReusedScope ReusedScope
            {
                get
                {
                    return _reusedScope;
                }
            }

            public void ReusedWithinNone()
            {
            }

            public void ReusedWithinContainer()
            {
                _reusedScope = new ContainerScope { Registration = this };
            }

            public void ReusedWithinHierarchy()
            {
                _reusedScope = new HierarchyScope { Registration = this };
            }

            public void ReusedWithin(object scope)
            {
                _reusedScope = new CustomScope(scope) { Registration = this };
            }

            public Registration<TFunc, TService> Clone<TFunc, TService>(Container container, ServiceKey serviceKey)
            {
                return ReusedScope.Clone<TFunc, TService>(container, serviceKey);
            }
        }

        private sealed class Registration<TFunc, TService> : Registration
        {
            private readonly TFunc _factory;
            private readonly Container _container;
            private TService _service;
            private bool _hasService;

            public Registration(Container container, TFunc factory, ReusedScope reusedScope)
                : base(reusedScope)
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

            public override Registration<TFunc, TService> Clone<TFunc, TService>(
                Container container, ServiceKey serviceKey)
            {
                var registration = (Registration<TFunc, TService>)Registration;

                if (registration.Container == container)
                {
                    return registration;
                }

                var clone = new Registration<TFunc, TService>(container, registration.Factory, new ContainerScope());
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

            public CustomScope(object scope)
            {
                _scope = scope;
                _canSave = false;
            }

            public override bool CanSave
            {
                get
                {
                    return _canSave;
                }
            }

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
                    new CustomScope(_scope) { _canSave = true });

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
                    }

                    current = current.Parent;
                }

                return scoped;
            }
        }
    }
}