using BookCatalog.Portal.ViewModel.Book;
using BookCatalog.ViewModel.DataTable;
using System.Collections.Generic;

namespace BookCatalog.Common.Business
{
    public interface IBookDM
    {
        BookVM GetBook(int bookId);

        DataTableResult<BookVM> GetBooksList(DataTableVM dataTableVM);

        BookVM Save(BookVM bookVM);

        void DeleteBook(int bookId);
    }
}
