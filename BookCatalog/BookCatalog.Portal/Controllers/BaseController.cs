using BookCatalog.Portal.Context;
using System.Web;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class BaseController : Controller
    {
        private readonly object mutex = new object();

        private DefaultContext context = default(DefaultContext);

        public HttpContextBase RequestContext
        {
            get
            {
                lock (this.mutex)
                {
                    if (this.context == null)
                    {
                        this.context = new DefaultContext();
                    }
                }

                return this.context;
            }
        }
    }
}