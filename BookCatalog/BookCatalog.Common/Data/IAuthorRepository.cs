using BookCatalog.Data.Entity.Author;
using System.Collections.Generic;

namespace BookCatalog.Common.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<AuthorEM> GetAll();

        void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds);

        void DeleteAuthor(int authorId);
    }
}
