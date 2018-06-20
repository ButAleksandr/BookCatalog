using BookCatalog.Data.Entity.Author;
using BookCatalog.Data.Entity.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookCatalog.Common.Data
{
    public interface IRepository
    {
        BookEM GetBook(int bookId);

        List<BookEM> GetBooks();

        Task<KeyValuePair<int, IEnumerable<AuthorEM>>> GetBookAuthors(int bookId);

        void DeleteBook(int bookId);
    }
}
