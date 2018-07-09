using BookCatalog.Common.Business;
using BookCatalog.Portal.ViewModel.Book;
using BookCatalog.ViewModel.DataTable;
using System.Text;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public partial class BookController : BaseController
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
        public PartialViewResult BookModal(int bookId)
        {
            return PartialView("_BookModal", bookId);
        }

        [HttpPost]
        public JsonResult GetBooksList(DataTableVM dataTableVM)
        {
            var bookDM = Factory.GetService<IBookDM>();

            return SerializeJson(bookDM.GetBooksList(dataTableVM));
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