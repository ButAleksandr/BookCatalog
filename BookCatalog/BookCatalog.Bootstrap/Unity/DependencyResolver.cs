using Microsoft.Practices.Unity;
using BookCatalog.Common.Business;
using BookCatalog.Common.Data;
using System.Configuration;

namespace BookCatalog.Bootstrap.Unity
{
    class DependencyResolver
    {
        public static void RegisterTypes(IUnityContainer _container)
        {
            InitDataLayer(_container);
            InitBusinessLayer(_container);
        }

        private static void InitDataLayer(IUnityContainer container)
        {
            var connString = ConfigurationManager.ConnectionStrings["BookCatalog"].ConnectionString;

            container.RegisterType<IRepository, Data.Repository>(new InjectionConstructor(connString));
        }

        private static void InitBusinessLayer(IUnityContainer container)
        {
            container.RegisterType<IBookDM, Business.DM.BookDM>(
                new InjectionConstructor(container.Resolve<IRepository>()));
        }
    }
}
