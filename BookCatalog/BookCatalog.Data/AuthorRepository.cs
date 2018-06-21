using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using BookCatalog.Data.Entity.Author;
using BookCatalog.Common.Data;

namespace BookCatalog.Data
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly string connString;

        public AuthorRepository(string connString)
        {
            this.connString = connString;
        }

        public IEnumerable<AuthorEM> GetAll()
        {
            var result = new List<AuthorEM>();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<AuthorEM>("Select * From [dbo].[Authors]").ToList();
            }

            return result;
        }

        public void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds)
        {
            var result = new List<AuthorEM>();
            const string deleteQuery = @"DELETE FROM [dbo].[AuthorsBooks]
                                WHERE BookId = @BookId",
                         insertQuery = @"INSERT INTO [dbo].[AuthorsBooks]
                                    ([AuthorId]
                                    ,[BookId])
                                VALUES
                                    (@AuthorId
                                    ,@BookId)";
            

            using (var db = new SqlConnection(this.connString))
            {
                db.Query(deleteQuery, new { BookId = bookId });
                
                foreach(int authorId in authorIds)
                {
                    db.Query(insertQuery, new { BookId = bookId, AuthorId = authorId });
                }
            }
        }
    }
}
