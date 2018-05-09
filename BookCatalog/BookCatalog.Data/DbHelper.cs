using System.Collections.Generic;
using BookCatalog.Data.Entity.Book.ViewModel;
using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace BookCatalog.Data
{
    public class DbHelper
    {
        private readonly string connString;

        public DbHelper(string connString)
        {
            this.connString = connString;
        }

        public List<BookWithAuthor> GetBooks()
        {
            var result = new List<BookWithAuthor>();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<BookWithAuthor>("Select * From [dbo].[BooksView]").ToList();
            }

            return result;
        }
    }
}
