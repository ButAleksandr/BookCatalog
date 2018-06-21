using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;

namespace BookCatalog.Common.Business
{
    public interface IAuthorDM
    {
        IEnumerable<AuthorVM> GetAll();

        void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds);
    }
}
