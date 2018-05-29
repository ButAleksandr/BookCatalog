using Microsoft.Practices.Unity;

namespace BookCatalog.Bootstrap.Unity
{
    class DependencyResolver
    {
        public static void RegisterTypes(IUnityContainer _container)
        {
            InitDataLayer(_container);
        }

        private static void InitDataLayer(IUnityContainer container)
        {
            container.RegisterType<Common.Data.IRepository, Data.Repository>();
        }

        private static void InitBusinessLayer(IUnityContainer container)
        {
            container.RegisterType<Common.Business.IBookDM, Business.DM.BookDM>();
        }
    }
}
