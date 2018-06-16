using BookCatalog.Common.Business;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class BookController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBooksList()
        {
            var bookDM = Factory.GetService<IBookDM>();

            var result = bookDM.GetBooksList();

            return Success(result);
        }

        [HttpGet]
        public ActionResult Edit(int bookId)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Delete(int bookId)
        {
            Factory.GetService<IBookDM>().DeleteBook(bookId);

            return Success();
        }
    }
}