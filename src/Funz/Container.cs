using System;

namespace Jwc.Funz
{
    public class Container
    {
        private object _result;

        public void Register<TService>(Func<Container, TService> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            _result = factory.Invoke(null);
        }

        public TService Resolve<TService>()
        {
            if (_result == null)
            {
                throw new ResolutionException(typeof(TService));    
            }

            return (TService)_result;
        }
    }
}