using BookCatalog.Data.Entity.Author;
using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;

namespace BookCatalog.Common.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<AuthorEM> GetAll();

        void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds);

        void DeleteAuthor(int authorId);

        AuthorEM Get(int author);

        bool AuthorIsExist(AuthorEM authorEM);
        AuthorEM Save(AuthorEM authorEM);
        AuthorEM Update(AuthorEM authorVM);
    }
}
