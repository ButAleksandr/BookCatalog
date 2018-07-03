using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;

namespace BookCatalog.Common.Business
{
    public interface IAuthorDM
    {
        IEnumerable<AuthorVM> GetAll();

        AuthorVM Get(int author);

        void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds);

        void DeleteAuthor(int bookId);
        AuthorVM Save(AuthorVM authorVM);
    }
}
