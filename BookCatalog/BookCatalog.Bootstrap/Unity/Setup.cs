using System;
using Unity;

namespace BookCatalog.Bootstrap.Unity
{
    class Setup
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
    }
}
