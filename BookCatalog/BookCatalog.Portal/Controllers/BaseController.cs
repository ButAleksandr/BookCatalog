using BookCatalog.Common.Bootstrap;
using BookCatalog.Common.Request;
using BookCatalog.Portal.Context;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static BookCatalog.Portal.Controllers.BookController;

namespace BookCatalog.Portal.Controllers
{
    public class BaseController : Controller
    {
        private readonly object mutex = new object();

        private DefaultContext context = default(DefaultContext);

        private IServiceProviderFactory _modelFactory = default(IServiceProviderFactory);

        public IRequestContext RequestContext
        {
            get
            {
                if (this.context == null)
                {
                    this.context = new DefaultContext();
                }

                return this.context;
            }
        }

        protected IServiceProviderFactory Factory
        {
            get
            {
                if (this._modelFactory == null)
                {
                    this._modelFactory = this.RequestContext.Factory;
                }

                return this._modelFactory;
            }
        }

        protected JsonResult Success(object model = null)
        {
            return new JsonResult()
            {
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json",
                Data = new { Massage = "OK", Value = model, IsSuccess = true },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }

        protected JsonResult Fail(object model = null, string message = null)
        {
            return new JsonResult()
            {
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/json",
                Data = new
                {
                    IsSuccess = false,
                    Model = model,
                    Message = message ?? string.Empty
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected JsonResult SerializeJson(object data)
        {
            return new JsonDataContractResult
            {
                Data = data,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}