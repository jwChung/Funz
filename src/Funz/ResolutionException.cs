using System;

namespace Jwc.Funz
{
    [Serializable]
    public class ResolutionException : Exception
    {
        private readonly Type _serviceType;
        private readonly object _key;

        public ResolutionException(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            _serviceType = serviceType;
        }

        public ResolutionException(Type serviceType, object key)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            _serviceType = serviceType;
            _key = key;
        }

        public Type ServiceType
        {
            get
            {
                return _serviceType;
            }
        }

        public object Key
        {
            get
            {
                return _key;
            }
        }
    }
}