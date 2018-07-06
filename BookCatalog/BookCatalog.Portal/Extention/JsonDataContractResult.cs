using System;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public partial class BookController
    {
        public class JsonDataContractResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                    String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Get is not allowed");
                }

                HttpResponseBase response = context.HttpContext.Response;

                if (!String.IsNullOrEmpty(ContentType))
                {
                    response.ContentType = ContentType;
                }
                else
                {
                    response.ContentType = "application/json";
                }
                if (ContentEncoding != null)
                {
                    response.ContentEncoding = ContentEncoding;
                }
                if (Data != null)
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(Data.GetType());
                    serializer.WriteObject(response.OutputStream, Data);
                }
            }
        }
    }
}