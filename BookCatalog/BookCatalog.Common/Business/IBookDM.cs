using BookCatalog.Portal.ViewModel.Book;
using System.Collections.Generic;

namespace BookCatalog.Common.Business
{
    public interface IBookDM
    {
        BookVM GetBook(int bookId);

        List<BookVM> GetBooksList();

        BookVM Save(BookVM bookVM);

        void DeleteBook(int bookId);
    }
}
