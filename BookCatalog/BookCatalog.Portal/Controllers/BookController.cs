using BookCatalog.Common.Business;
using BookCatalog.Portal.ViewModel.Book;
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
        public ActionResult GetBook(int bookId)
        {
            var bookDM = Factory.GetService<IBookDM>();
            var authorDM = Factory.GetService<IAuthorDM>();

            if(bookId >= 0)
            {
                var result = bookId == 0 
                    ? new BookVM() 
                    : bookDM.GetBook(bookId);

                result.AllAuthors = authorDM.GetAll();

                return Success(result);
            }

            return Fail(null, "Bad 'bookId' property value.");
        }

        [HttpGet]
        public ActionResult GetBooksList()
        {
            var bookDM = Factory.GetService<IBookDM>();

            var result = bookDM.GetBooksList();

            return Success(result);
        }

        [HttpPost]
        public ActionResult Save(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                var bookDM = Factory.GetService<IBookDM>();
                var authorDM = Factory.GetService<IAuthorDM>();

                var result = bookDM.Save(bookVM);
                authorDM.UpdateBookAuthors(result.Id, bookVM.AuthorIds);

                return Success(result);
            }

            return Fail(bookVM, "ModelState.IsValid = False");
        }

        [HttpGet]
        public ActionResult Delete(int bookId)
        {
            Factory.GetService<IBookDM>().DeleteBook(bookId);

            return Success();
        }
    }
}