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

        public AuthorEM Get(int authorId)
        {
            using (var db = new SqlConnection(this.connString))
            {
                return db.Query<AuthorEM>("Select * From [dbo].[Authors] WHERE [Id] = @authorId", new { authorId }).FirstOrDefault();
            }
        }

        public AuthorEM Save(AuthorEM authorEM)
        {
            const string query = @"
                                INSERT INTO [dbo].[Authors]
                                            ([FirstName]
                                             ,[LastName]
                                             ,[BookCount])
                                        VALUES
                                            (@FirstName
                                            ,@LastName
                                            ,@BookCount)

                                Select SCOPE_IDENTITY();";

            using (var db = new SqlConnection(this.connString))
            {
                int newAuthorId = db.Query<int>(query, authorEM).FirstOrDefault();

                return Get(newAuthorId);
            }
        }

        public AuthorEM Update(AuthorEM authorEM)
        {
            const string query = @"
                                 UPDATE [dbo].[Authors]
                                   SET [FirstName] = @FirstName
                                      ,[LastName] = @LastName
                                 WHERE [Id] = @Id;";

            using (var db = new SqlConnection(this.connString))
            {
                db.Query<int>(query, authorEM).FirstOrDefault();

                return Get(authorEM.Id);
            }
        }
        
        public bool AuthorIsExist(AuthorEM authorEM)
        {
            const string query = @"SELECT Count(1) FROM [dbo].[Authors] WHERE [Id] = @Id";
            bool isExist;

            using (var db = new SqlConnection(this.connString))
            {
                isExist = db.Query<int>(query, new { authorEM.Id }).First() > 0;
            }

            return isExist;
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

        public void DeleteAuthor(int authorId)
        {
            using (var db = new SqlConnection(this.connString))
            {
                db.Execute("EXEC [dbo].[Delete_Author] @authorId", new { authorId });
            }
        }
    }
}
