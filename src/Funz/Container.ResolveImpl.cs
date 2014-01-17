using System;

namespace Jwc.Funz
{
    public partial class Container
    {
        /* All ResolveImpl are essentially equal, except for the type of the factory 
         * which is "hardcoded" in each implementation. This slight repetition of 
         * code gives us a bit more of perf. gain by avoiding an intermediate 
         * func/lambda to call in a generic way as we did before.
         */

        private TService ResolveImpl<TService, TArg1, TArg2>(object key, bool throws, TArg1 arg1, TArg2 arg2)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            registration.Service = service;
            return service;
        }

        private TService ResolveImpl<TService, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14>(object key, bool throws, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10, TArg11 arg11, TArg12 arg12, TArg13 arg13, TArg14 arg14)
        {
            var registration = GetRegistration<Func<Container, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TService>, TService>(key, throws);
            if (registration == null)
			    return default(TService);

            if (registration.HasService)
                return registration.Service;

            var service = registration.Factory.Invoke(this, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            registration.Service = service;
            return service;
        }

    }
}
