using System.Collections.Generic;
using BookCatalog.Data.Entity.Book.ViewModel;
using BookCatalog.Common.Interfaces.Data;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using BookCatalog.Common.Interfaces.ViewModel;
using BookCatalog.Common.Interfaces.Entity;
using BookCatalog.Data.Entity.Author;
using System.Threading.Tasks;

namespace BookCatalog.Data
{
    public class DbHelper : IDbHelper
    {
        private readonly string connString;

        public DbHelper(string connString)
        {
            this.connString = connString;
        }

        IEnumerable<IBookWithAuthor> IDbHelper.GetBooks()
        {
            var result = new List<BookWithAuthor>();

            using (var db = new SqlConnection(this.connString))
            {
                result = db.Query<BookWithAuthor>("Select * From [dbo].[BooksView]").ToList();

                FillBooksAuthros(result);
            }

            return result;
        }

        #region Helpers

        private void FillBooksAuthros(IEnumerable<IBookWithAuthor> books)
        {
            Dictionary<int, IEnumerable<Author>> authors = GetBooksAuthors(books);

            foreach(IBookWithAuthor book in books)
            {
                book.Authors = authors[book.Id];
            }
        }

        private Dictionary<int, IEnumerable<Author>> GetBooksAuthors(IEnumerable<IBook> bookIds)
        {
            var taskList = new List<Task<KeyValuePair<int, IEnumerable<Author>>>>();
            var booksAuthors = new Dictionary<int, IEnumerable<Author>>();

            foreach (int id in bookIds.Select(x => x.Id))
            {
                taskList.Add(GetBookAuthors(id));
            }

            var taskArray = taskList.ToArray();

            Task.WaitAll(taskArray);

            foreach (Task<KeyValuePair<int, IEnumerable<Author>>> task in taskArray)
            {
                var pair = task.Result;

                booksAuthors.Add(pair.Key, pair.Value);
            }

            return booksAuthors;
        }

        private async Task<KeyValuePair<int, IEnumerable<Author>>> GetBookAuthors(int bookId)
        {
            const string query = @"SELECT * FROM [dbo].[GetAuthors] (@bookId)";

            using (var db = new SqlConnection(this.connString))
            {
                var getAuthorsTask = db.QueryAsync<Author>(query, new { bookId });
               
                var keyValuePair = new KeyValuePair<int, IEnumerable<Author>>(bookId, await getAuthorsTask);

                return keyValuePair;
            }
        }

        #endregion

    }
}
