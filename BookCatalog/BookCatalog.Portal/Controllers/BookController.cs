using BookCatalog.Common.Business;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class BookController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetBooksList()
        {
            var bookDM = Factory.GetService<IBookDM>();

            var result = bookDM.GetBooksList();

            return Success(result);
        }
    }
}