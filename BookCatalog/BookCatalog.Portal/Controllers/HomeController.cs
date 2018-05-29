using BookCatalog.Business.DM;
using BookCatalog.Common.Business;
using BookCatalog.Common.Data;
using System.Configuration;
using System.Web.Mvc;

namespace BookCatalog.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBookDM bookDM;

        public HomeController()
        {
            this.bookDM = Factory.GetService<IBookDM>();

            var books = bookDM.GetBooksList();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}