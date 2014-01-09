using System;

namespace Jwc.Funz
{
    [Serializable]
    public class ResolutionException : Exception
    {
        private readonly Type _serviceType;

        public ResolutionException(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            _serviceType = serviceType;
        }

        public Type ServiceType
        {
            get
            {
                return _serviceType;
            }
        }
    }
}