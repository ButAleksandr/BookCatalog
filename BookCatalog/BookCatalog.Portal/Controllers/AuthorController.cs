using BookCatalog.Common.Business;
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
        public ActionResult Delete(int authorId)
        {
            Factory.GetService<IAuthorDM>().DeleteAuthor(authorId);

            return Success();
        }        
    }
}