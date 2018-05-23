using BookCatalog.Business.DM;
using BookCatalog.Common.Data;
using BookCatalog.Data.Entity.Author;
using BookCatalog.Data.Entity.Book;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalog.Data.Test
{
    [TestClass]
    public class BookDMTest
    {
        private readonly Mock<IRepository> repositoryMock;

        public BookDMTest()
        {
            repositoryMock = new Mock<IRepository>();
        }

        [TestMethod]
        public void GetBooksTest()
        {
            // Arrange
            var books = GenerateBooks();
            var authors = GenerateAuthors();
            var relations = GenerateRelations(books, authors, 10);
            repositoryMock
                .Setup(c => c.GetBooks())
                .Returns(books);
            repositoryMock
                .Setup(c => c.GetBookAuthors(It.IsAny<int>()))
                .Returns((int bookId) => {
                    return Task.FromResult(relations.FirstOrDefault(x => x.Key == bookId));
                });
            var bookDm = new BookDM(repositoryMock.Object);

            // Act
            var result = bookDm.GetBooksList();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Count(x => books.Any(y => y.Id == x.Id)) == books.Count);
            Assert.IsTrue(result.All(x => x.Authors.Count() == relations.First(y => y.Key == x.Id).Value.Count()));
        }

        private List<BookEM> GenerateBooks(int count = 20)
        {
            var testBooks = new List<BookEM>();
            var random = new Random();

            if (count > 0)
            {
                foreach (int index in Enumerable.Range(1, count))
                {
                    testBooks.Add(new BookEM()
                    {
                        Id = index,
                        Name = Guid.NewGuid().ToString(),
                        PageCount = random.Next(50, 1500),
                        ReleaseDate = new DateTime(
                            random.Next(1600, DateTime.Now.Year),
                            random.Next(1, 12),
                            random.Next(1, 28))
                    });
                }
            }

            return testBooks;
        }

        private List<AuthorEM> GenerateAuthors(int count = 100)
        {
            var testAuthors = new List<AuthorEM>();
            var random = new Random();

            if (count > 0)
            {
                foreach (int index in Enumerable.Range(1, count))
                {
                    testAuthors.Add(new AuthorEM()
                    {
                        Id = index,
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        BookCount = 0
                    });
                }
            }

            return testAuthors;
        }

        private List<KeyValuePair<int, IEnumerable<AuthorEM>>> GenerateRelations(
            List<BookEM> books, 
            List<AuthorEM> authors, 
            int maxAuthorCount)
        {
            var resultRelations = new List<KeyValuePair<int, IEnumerable<AuthorEM>>>();
            var random = new Random();

            resultRelations = books.Select(b => new KeyValuePair<int, IEnumerable<AuthorEM>>(
                b.Id, 
                Enumerable.Range(1, random.Next(1, maxAuthorCount))
                    .Select(x => random.Next(0, authors.Count))
                    .Select(x => authors[x]))).ToList();

            return resultRelations;
        }
    }
}
