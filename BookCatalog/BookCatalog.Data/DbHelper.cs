using System.Collections.Generic;
using BookCatalog.Data.Entity.Book;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

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

        #region Helpers

        public Dictionary<int, string> GetBooksAuthors(IEnumerable<Book> bookIds)
        {
            var taskList = new List<Task<KeyValuePair<int, string>>>();
            var booksAuthors = new Dictionary<int, string>();

            foreach (int id in bookIds.Select(x => x.Id))
            {
                taskList.Add(GetBookAuthors(id));
            }

            var taskArray = taskList.ToArray();

            Task.WaitAll(taskArray);

            foreach(Task<KeyValuePair<int, string>> task in taskArray)
            {
                var pair = task.Result;

                booksAuthors.Add(pair.Key, pair.Value);
            }

            return booksAuthors;
        }

        private async Task<KeyValuePair<int, string>> GetBookAuthors(int bookId)
        {
            const string query = @"
                SELECT [FirstName] + ' ' + [LastName]
                FROM [dbo].[Author]
                WHERE [Id] IN (SELECT [AuthorId]
                               FROM [dbo].[AuthorsBooks]
                               WHERE [BookId] = @bookId)";

            using (var db = new SqlConnection(this.connString))
            {
                var getAuthorsTask = db.QueryAsync<string>(query, new { bookId });
                // Trim function using on situation when last or first name is empty
                var authorsList = (await getAuthorsTask).Select(x => x.Trim()); 

                var keyValuePair = new KeyValuePair<int, string>
                    (bookId, string.Join(", ", authorsList));

                return keyValuePair;
            }
        }

        #endregion

    }
}
