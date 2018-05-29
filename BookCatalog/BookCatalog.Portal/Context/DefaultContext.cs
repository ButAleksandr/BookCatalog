using System;
using System.Web;

namespace BookCatalog.Portal.Context
{
    public class DefaultContext : HttpContextBase
    {
        public IServiceProvider Factory { get; set; }
    }
}