using BookCatalog.Bootstrap.Unity;
using BookCatalog.Common.Bootstrap;
using BookCatalog.Common.Data;
using BookCatalog.Common.Request;
using System;
using System.Security.Principal;
using System.Web;

namespace BookCatalog.Portal.Context
{
    public class DefaultContext : HttpContextBase, IRequestContext
    {
        public IServiceProviderFactory Factory { get; set; }     

        public DefaultContext() : base()
        {
            this.Factory = Setup.CreateFactory(this);
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}