using BookCatalog.Portal.ViewModel.Book;
using System.Collections.Generic;

namespace BookCatalog.Common.Business
{
    public interface IBookDM
    {
        List<BookVM> GetBooksList();

        void DeleteBook(int bookId);
    }
}
