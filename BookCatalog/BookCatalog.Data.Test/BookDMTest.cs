using BookCatalog.Business.DM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BookCatalog.Data.Test
{
    [TestClass]
    public class BookDMTest
    {
        private readonly string connString;

        public BookDMTest()
        {
            this.connString = System.Configuration.ConfigurationManager
                .ConnectionStrings["BookCatalog"].ConnectionString;
        }

        [TestMethod]
        public void GetBooksTest()
        {
            // Arrange
            var bookDm = new BookDM(this.connString);

                    
            using(var manager = new TestDataManager(connString))
            {
                string bookName = Guid.NewGuid().ToString();
                string authorFirstName = Guid.NewGuid().ToString();
                string authorLastName = Guid.NewGuid().ToString();

                int bookId = manager.CreateBook(bookName);
                int authorId = manager.CreateAuthor(authorFirstName, authorLastName);
                manager.CreateBookAuthorRelation(bookId, authorId);

                // Act
                var result = bookDm.GetBooksList();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Count > 0);
                Assert.IsTrue(result.Any(x => x.Name == bookName));
                Assert.IsTrue(result.Where(x => x.Name == bookName)
                    .Any(x => x.Authors
                        .Any(y => y.FirstName == authorFirstName && y.LastName == authorLastName)));
            }            
        }
    }
}
