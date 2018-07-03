using BookCatalog.Common.Business;
using BookCatalog.Portal.ViewModel.Author;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class AuthorController : BaseController
    {
        public ActionResult Index()
        {
            return View("AuthorList");
        }

        [HttpGet]
        public ActionResult AuthorsList()
        {
            var dm = Factory.GetService<IAuthorDM>();

            var result = dm.GetAll();

            return Success(result);
        }

        [HttpGet]
        public ActionResult Get(int authorId)
        {
            var authorDM = Factory.GetService<IAuthorDM>();

            if (authorId >= 0)
            {
                var result = authorId == 0
                    ? new AuthorVM()
                    : authorDM.Get(authorId);

                return Success(result);
            }

            return Fail(null, "Bad 'authorId' property value.");
        }

        [HttpPost]
        public ActionResult Save(AuthorVM authorVM)
        {
            if (ModelState.IsValid)
            {
                var authorDM = Factory.GetService<IAuthorDM>();

                var result = authorDM.Save(authorVM);

                return Success(result);
            }

            return Fail(authorVM, "ModelState.IsValid = False");
        }

        [HttpGet]
        public ActionResult Delete(int authorId)
        {
            Factory.GetService<IAuthorDM>().DeleteAuthor(authorId);

            return Success();
        }        
    }
}