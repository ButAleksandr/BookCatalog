using BookCatalog.Common.Bootstrap;
using Microsoft.Practices.Unity;
using System;

namespace BookCatalog.Bootstrap.Unity
{
    public class Setup
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static void RegisterTypes(IUnityContainer container)
        {
            DependencyResolver.RegisterTypes(container);
        }

        public static IUnityContainer GetUnityConfig()
        {
            return container.Value;
        }

        public static IServiceProviderFactory CreateFactory(IInternalRequestContext context)
        {
            return new ServiceProviderFactory(container.Value, context);
        }
    }
}
