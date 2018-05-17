using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BookCatalog.Data.Test
{
    class TestDataManager : IDisposable
    {
        private List<int> createdBookIds;
        private List<int> createdAuthorIds;
        private readonly string connString;

        public TestDataManager(string connString)
        {
            createdBookIds = new List<int>();
            createdAuthorIds = new List<int>();

            this.connString = connString;
        }

        public int CreateBook(string bookName)
        {
            string query = @"INSERT INTO [dbo].[Books] ([Name], [PageCount], [ReleaseDate], [Rate])
                             VALUES (@bookName, 0, GetDate(), 0)
                            Select SCOPE_IDENTITY()";

            using (var db = new SqlConnection(this.connString))
            {
                var id = db.Query<int>(query, new { bookName = bookName }).First();

                createdBookIds.Add(id);

                return id;
            }
        }

        public int CreateAuthor(string firstName, string lastName)
        {
            string query = @"INSERT INTO [dbo].[Authors] ([FirstName], [LastName],[BookCount])
                            VALUES (@firstName, @lastName,0)
                            Select SCOPE_IDENTITY()";

            using (var db = new SqlConnection(this.connString))
            {
                var id = db.Query<int>(query, new { firstName, lastName }).First();

                createdAuthorIds.Add(id);

                return id;
            }            
        }

        public void CreateBookAuthorRelation(int bookId, int authorId)
        {
            string query = @"INSERT INTO [dbo].[AuthorsBooks] ([AuthorId], [BookId])
                            VALUES (@authorId, @bookId)";

            using (var db = new SqlConnection(this.connString))
            {
                db.Query(query, new { authorId, bookId });
            }
        }

        private void DisposeBooks()
        {
            string query = @"DELETE FROM [dbo].[Books] 
                            WHERE [Id] = @bookId";

            using (var db = new SqlConnection(this.connString))
            {
                foreach (int bookId in createdBookIds)
                {
                    db.Query(query, new { bookId });
                }
            }
        }

        private void DisposeRelations()
        {
            string query = @"DELETE FROM [dbo].[AuthorsBooks] 
                            WHERE [BookId] = @bookId";

            using (var db = new SqlConnection(this.connString))
            {
                foreach(int bookId in createdBookIds)
                {
                    db.Query(query, new { bookId });
                }
            }
        }

        private void DisposeAuthors()
        {
            string query = @"DELETE FROM [dbo].[Authors] 
                            WHERE [Id] = @authorId";

            using (var db = new SqlConnection(this.connString))
            {
                foreach (int authorId in createdAuthorIds)
                {
                    db.Query(query, new { authorId });
                }
            }
        }

        public void Dispose()
        {
            DisposeRelations();
            DisposeAuthors();
            DisposeBooks();
        }
    }
}
