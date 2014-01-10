﻿using System;

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
        private readonly object _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        /// <param name="serviceType">
        /// The service type failed to resolve.
        /// </param>
        public ResolutionException(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            _serviceType = serviceType;
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
    }
}