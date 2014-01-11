using System;
using System.Collections.Generic;

namespace Jwc.Funz
{
    /// <summary>
    /// Exception thrown by the container when a service cannot be resolved.
    /// </summary>
    [Serializable]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1032:ImplementStandardExceptionConstructors",
        Justification = "Without the constructors but with a service type and a key, " +
                        "this class can represent a standard exception message")]
    public class ResolutionException : Exception
    {
        [NonSerializedAttribute]
        private readonly Type _serviceType;

        [NonSerializedAttribute]
        private readonly Type[] _argumentTypes;

        [NonSerializedAttribute]
        private readonly object _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="serviceType">
        /// The service type failed to resolve.
        /// </param>
        /// <param name="argumentTypes">
        /// The argument types to be used to construct a service instance.
        /// </param>
        public ResolutionException(Type serviceType, Type[] argumentTypes)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            if (argumentTypes == null)
            {
                throw new ArgumentNullException("argumentTypes");
            }

            _serviceType = serviceType;
            _argumentTypes = argumentTypes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="serviceType">
        /// The service type failed to resolve an service instance.
        /// </param>
        /// <param name="key">
        /// The key failed to resolve an service instance.
        /// </param>
        /// <param name="argumentTypes">
        /// The argument types to be used to construct a service instance.
        /// </param>
        public ResolutionException(Type serviceType, object key, Type[] argumentTypes)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (argumentTypes == null)
            {
                throw new ArgumentNullException("argumentTypes");
            }

            _serviceType = serviceType;
            _key = key;
            _argumentTypes = argumentTypes;
        }

        /// <summary>
        /// Gets a value indicating the service type of which instance
        /// container failed to resolve.
        /// </summary>
        public Type ServiceType
        {
            get
            {
                return _serviceType;
            }
        }

        /// <summary>
        /// Gets a value indicating the registration key with which container
        /// failed to resolve.
        /// </summary>
        public object Key
        {
            get
            {
                return _key;
            }
        }

        /// <summary>
        /// Gets a value inicating the argument types to be used to construct a service instance.
        /// </summary>
        public IList<Type> ArgumentTypes
        {
            get
            {
                return _argumentTypes;
            }
        }
    }
}