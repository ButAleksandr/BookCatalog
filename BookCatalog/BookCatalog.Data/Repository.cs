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
                var getAuthorsTask = db.QueryAsync<AuthorEM>(query, new { bookId });

                var keyValuePair = new KeyValuePair<int, IEnumerable<AuthorEM>>(bookId, await getAuthorsTask);

                return keyValuePair;
            }
        }
    }
}
