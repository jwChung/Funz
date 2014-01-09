using System;
using System.Collections.Generic;

namespace Jwc.Funz
{
    public class Container
    {
        private static readonly object _noKey = new object();
        private readonly IDictionary<ServiceKey, object> _registry;

        public Container()
        {
            _registry = new Dictionary<ServiceKey, object>();
        }

        public void Register<TService>(Func<Container, TService> factory)
        {
            RegisterImpl<TService>(_noKey, factory);
        }

        public void Register<TService, TArg>(Func<Container, TArg, TService> factory)
        {
            RegisterImpl<TService>(_noKey, factory);
        }

        public void Register<TService>(object key, Func<Container, TService> factory)
        {
            RegisterImpl<TService>(key, factory);
        }

        public void Register<TService, TArg>(object key, Func<Container, TArg, TService> factory)
        {
            RegisterImpl<TService>(key, factory);
        }

        public TService Resolve<TService>()
        {
            return ResolveImpl<Func<Container, TService>, TService>(_noKey)
                .Invoke(this);
        }

        public TService Resolve<TService, TArg>(TArg arg)
        {
            return ResolveImpl<Func<Container, TArg, TService>, TService>(_noKey)
                .Invoke(this, arg);
        }

        public TService ResolveKeyed<TService>(object key)
        {
            return ResolveImpl<Func<Container, TService>, TService>(key)
                .Invoke(this);
        }

        public TService ResolveKeyed<TService, TArg>(object key, TArg arg)
        {
            return ResolveImpl<Func<Container, TArg, TService>, TService>(key)
                .Invoke(this, arg);
        }

        private void RegisterImpl<TService>(object key, object factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            _registry[new ServiceKey(typeof(TService), key)] = factory;
        }

        private TFunc ResolveImpl<TFunc, TService>(object key)
        {
            object service;
            if (!_registry.TryGetValue(new ServiceKey(typeof(TService), key), out service))
            {
                throw new ResolutionException(typeof(TService), key);
            }

            return (TFunc)service;
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
            
            private bool Equals(ServiceKey other)
            {
                return _servideType == other._servideType && _key.Equals(other._key);
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
                    return (_servideType.GetHashCode()*397) ^ _key.GetHashCode();
                }
            }
        }
    }
}