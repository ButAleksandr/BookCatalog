using System.Collections.Generic;
using BookCatalog.Common.Interfaces.Entity;
using BookCatalog.Common.Interfaces.ViewModel;

namespace BookCatalog.Data.Entity.Book.ViewModel
{
    public class BookWithAuthor : Book, IBookWithAuthor
    {
        IEnumerable<IAuthor> IBookWithAuthor.Authors { get; set; }
    }
}
