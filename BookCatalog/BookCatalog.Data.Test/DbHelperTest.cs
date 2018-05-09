using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookCatalog.Data.Entity.Book.ViewModel;
using System.Collections.Generic;

namespace BookCatalog.Data.Test
{
    [TestClass]
    public class DbHelperTest
    {
        private readonly string connString;

        public DbHelperTest()
        {
            this.connString = System.Configuration.ConfigurationManager
                .ConnectionStrings["BookCatalog"].ConnectionString;
        }

        [TestMethod]
        public void GetBooksTest()
        {
            var dbHelper = new DbHelper(this.connString);

            List<BookWithAuthor> books = dbHelper.GetBooks();

            Assert.IsNotNull(books);
        }
    }
}
