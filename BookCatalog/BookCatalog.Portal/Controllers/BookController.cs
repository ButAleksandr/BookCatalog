using BookCatalog.Common.Business;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class BookController : BaseController
    {
        public ActionResult Index()
        {
            return ShowBooksList();
        }

        public ActionResult ShowBooksList()
        {
            var bookDM = Factory.GetService<IBookDM>();

            return null;
        }
    }
}