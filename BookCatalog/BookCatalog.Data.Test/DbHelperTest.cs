using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookCatalog.Data.Entity.Book.ViewModel;
using System.Collections.Generic;
using BookCatalog.Common.Interfaces.Data;

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
            IDbHelper dbHelper = new DbHelper(this.connString);

            // Fill books fields
            List<BookWithAuthor> books = (List<BookWithAuthor>)dbHelper.GetBooks();
            
            Assert.IsNotNull(books);
        }
    }
}
