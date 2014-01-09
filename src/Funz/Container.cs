using System;
using System.Collections.Generic;

namespace Jwc.Funz
{
    public class Container
    {
        private readonly Container _parent;
        private static readonly object _noKey = new object();
        private readonly IDictionary<ServiceKey, Registration> _registry;

        public Container()
            : this(null)
        {
        }

        private Container(Container parent)
        {
            _registry = new Dictionary<ServiceKey, Registration>();
            _parent = parent;
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
            return new Container(this);
        }

        private IRegistration RegisterImpl<TFunc, TService>(object key, TFunc factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            var registration = new Registration<TFunc, TService>(factory, this);
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
            Registration value = null;
            while (current != null && !current._registry.TryGetValue(serviceKey, out value))
            {
                current = current._parent;
            }

            if (value == null)
            {
                throw new ResolutionException(typeof(TService), serviceKey.Key);
            }

            var registration = (Registration<TFunc, TService>)value;

            if (registration.Reused == ReusedScope.Container && current != this)
            {
                var clone = new Registration<TFunc, TService>(registration.Factory, this, ReusedScope.Container);
                _registry[serviceKey] = clone;
                return clone;
            }

            return registration;
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
                    return (_servideType.GetHashCode()*397) ^ Key.GetHashCode();
                }
            }
        }

        private abstract class Registration : IRegistration
        {
            private ReusedScope _reused;

            protected Registration(ReusedScope reused)
            {
                _reused = reused;
            }
            
            public ReusedScope Reused
            {
                get
                {
                    return _reused;
                }
            }

            public void ReusedWithinNone()
            {
            }

            public void ReusedWithinContainer()
            {
                _reused = ReusedScope.Container;
            }

            public void ReusedWithinHierarchy()
            {
                _reused = ReusedScope.Hierarchy;
            }
        }

        private class Registration<TFunc, TService> : Registration
        {
            private readonly TFunc _factory;
            private readonly Container _container;
            private TService _service;
            private bool _hasService;

            public Registration(TFunc factory, Container container, ReusedScope reused = ReusedScope.None)
                : base(reused)
            {
                _factory = factory;
                _container = container;
                _hasService = false;
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
                    if (Reused == ReusedScope.None)
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
        }

        private enum ReusedScope
        {
            None,
            Container,
            Hierarchy
        }
    }
}