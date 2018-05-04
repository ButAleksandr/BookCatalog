using System.Collections.Generic;
using BookCatalog.Data.Entity.Book;
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

        public List<Book> GetBooks()
        {
            var result = new List<Book>();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<Book>("Select * From [dbo].[Books]").ToList();
            }

            return result;
        }
    }
}
