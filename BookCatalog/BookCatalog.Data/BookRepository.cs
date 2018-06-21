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
    public class BookRepository : IBookRepository
    {
        private readonly string connString;

        public BookRepository(string connString)
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

        public bool BookIsExist(BookEM bookEM)
        {
            const string query = @"SELECT Count(1) FROM [dbo].[Books] WHERE [Id] = @Id";
            bool bookIsExist;

            using (var db = new SqlConnection(this.connString))
            {
                bookIsExist = db.Query<int>(query, new { bookEM.Id } ).First() > 0;
            }

            return bookIsExist;
        }
        public BookEM Save(BookEM bookEM)
        {
            const string query = @"
                                INSERT INTO [dbo].[Books]
                                            ([Name]
                                            ,[PageCount]
                                            ,[ReleaseDate]
                                            ,[Rate])
                                        VALUES
                                            (@Name
                                            ,@PageCount
                                            ,@ReleaseDate
                                            ,@Rate)

                                Select SCOPE_IDENTITY();";

            using (var db = new SqlConnection(this.connString))
            {
                int newBookId = db.Query<int>(query, bookEM).FirstOrDefault();

                return GetBook(newBookId);
            }
        }

        public BookEM Update(BookEM bookEM)
        {
            const string query = @"
                                UPDATE [dbo].[Books]
                                   SET [Name] = @Name
                                      ,[PageCount] = @PageCount
                                      ,[ReleaseDate] = @ReleaseDate
                                      ,[Rate] = @Rate
                                 WHERE [Id] = @Id;";

            using (var db = new SqlConnection(this.connString))
            {
                db.Query<int>(query, bookEM).FirstOrDefault();

                return GetBook(bookEM.Id);
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
