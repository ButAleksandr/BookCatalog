using BookCatalog.Data.Entity.Author;
using BookCatalog.Data.Entity.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Common.Data
{
    public interface IRepository
    {
        List<BookEM> GetBooks();

        Task<KeyValuePair<int, IEnumerable<AuthorEM>>> GetBookAuthors(int bookId);
    }
}
