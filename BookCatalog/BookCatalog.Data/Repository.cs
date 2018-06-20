using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using BookCatalog.Data.Entity.Author;
using System.Threading.Tasks;
using BookCatalog.Data.Entity.Book;
using BookCatalog.Common.Data;

namespace BookCatalog.Data
{
    public class Repository : IRepository
    {
        private readonly string connString;

        public Repository(string connString)
        {
            this.connString = connString;
        }

        public BookEM GetBook(int bookId)
        {
            var result = new BookEM();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<BookEM>("Select * From [dbo].[BooksView] where [Id] = @Id",
                        new { Id = bookId })
                        .First()
                    ?? result;
            }

            return result;
        }

        public List<BookEM> GetBooks()
        {
            var result = new List<BookEM>();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<BookEM>("Select * From [dbo].[BooksView]").ToList();
            }

            return result;
        }

        public async Task<KeyValuePair<int, IEnumerable<AuthorEM>>> GetBookAuthors(int bookId)
        {
            const string query = @"SELECT * FROM [dbo].[GetAuthors] (@bookId)";

            using (var db = new SqlConnection(this.connString))
            {
                var authors = db.Query<AuthorEM>(query, new { bookId });

                var keyValuePair = new KeyValuePair<int, IEnumerable<AuthorEM>>(bookId, authors);

                return await Task.FromResult(keyValuePair);
            }
        }

        public void DeleteBook(int bookId)
        {
            var result = new List<BookEM>();

            using (var db = new SqlConnection(this.connString))
            {
                db.Execute("EXEC [dbo].[Delete_Book] @bookId", new { bookId = bookId });
            }
        }
    }
}
