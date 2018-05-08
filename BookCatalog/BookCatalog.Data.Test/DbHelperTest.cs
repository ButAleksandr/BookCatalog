using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookCatalog.Data.Entity.Book;
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

            List<Book> books = dbHelper.GetBooks();

            Assert.IsNotNull(books);
        }

        [TestMethod]
        public void GetBooksAuthorsTest()
        {
            var dbHelper = new DbHelper(this.connString);

            List<Book> books = dbHelper.GetBooks();
            Dictionary<int, string> booksAuthors = dbHelper.GetBooksAuthors(books);

            Assert.IsNotNull(books);
            Assert.IsNotNull(booksAuthors);
        }
    }
}
