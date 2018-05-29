using BookCatalog.Common.Bootstrap;
using BookCatalog.Common.Business;
using Microsoft.Practices.Unity;
using System;

namespace BookCatalog.Bootstrap
{
    internal sealed class ServiceProviderFactory : IServiceProviderFactory, IDisposable
    {
        #region Private declarations

        /// <summary>
        /// Stores an instance of data context factory
        /// </summary>
        private readonly IInternalRequestContext requestContext = default(IInternalRequestContext);

        /// <summary>
        /// Stores cached value of dataContext factory
        /// </summary>
        private readonly ServiceProviderParametersResolver DefaultResolver = default(ServiceProviderParametersResolver);

        /// <summary>
        /// Stores static instance of builder
        /// </summary>
        private readonly IUnityContainer container = new UnityContainer();

        /// <summary>
        /// Stores a cached instance type metadata
        /// </summary>
        private static readonly Type TypedTrigger = typeof(IBusinessContext);

        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="ServiceProviderFactory"/> class
        /// </summary>
        /// <param name="container">Instance of container</param>
        /// <param name="requestContext">Current user context</param>
        public ServiceProviderFactory(IUnityContainer container, IInternalRequestContext requestContext)
        {
            this.container = container;
            this.requestContext = requestContext;

            this.DefaultResolver = new ServiceProviderParametersResolver(requestContext, null);
        }

        #region IServiceProvider Members

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns> A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
        public object GetService(Type serviceType)
        {
            var paremeters = new ServiceProviderParametersResolver(this.requestContext, null, this.isSelectedType(serviceType));
            return this.container.Resolve(serviceType, paremeters);
        }

        #endregion

        #region Custom IServiceProvider Members

        public object GetService(Type serviceType, params object[] constructParams)
        {
            return this.container.Resolve(serviceType, new ServiceProviderParametersResolver(this.requestContext, constructParams, this.isSelectedType(serviceType)));
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="serviceType">An object that specifies the type of service object to get.</typeparam>
        /// <returns> A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
        public serviceType GetService<serviceType>()
        {
            return (serviceType)this.GetService(typeof(serviceType));
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="TService">An object that specifies the type of service object to get.</typeparam>
        /// <param name="constructParams">List of construction arguments</param>
        /// <returns>A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
        public TService GetService<TService>(params object[] constructParams)
        {
            return this.container.Resolve<TService>(new ServiceProviderParametersResolver(this.requestContext, constructParams, this.isSelectedType(typeof(TService))));
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="TService">An object that specifies the type of service object to get.</typeparam>
        /// <param name="name">Mapping name for the IoC resolver.</param>
        /// <param name="param">List of construction arguments</param>
        /// <returns>A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
        public TService GetMappingService<TService>(string name, params object[] param)
        {
            return this.container.Resolve<TService>(name, new ServiceProviderParametersResolver(this.requestContext, param, this.isSelectedType(typeof(TService))));
        }

        public TService TryGetMappingService<TService>(string name, params object[] param)
        {
            try
            {
                return this.container.Resolve<TService>(name, new ServiceProviderParametersResolver(this.requestContext, param, this.isSelectedType(typeof(TService))));
            }
            catch (Exception)
            {
                return default(TService);
            }
        }

        #endregion Custom IServiceProvider Members

        private bool isSelectedType(Type serviceType)
        {
            if (serviceType == null)
            {
                return false;
            }

            if (TypedTrigger.IsAssignableFrom(serviceType))
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposeManaged)
        {
            //TODO: Add release code here
        }

        ~ServiceProviderFactory()
        {
            this.Dispose(false);
        }
    }
}
