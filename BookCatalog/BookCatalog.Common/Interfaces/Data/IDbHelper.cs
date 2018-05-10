namespace BookCatalog.Common.Interfaces.Data
{
    using System.Collections.Generic;

    public interface IDbHelper
    {
        IEnumerable<ViewModel.IBookWithAuthor> GetBooks();
    }
}
