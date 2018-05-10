namespace BookCatalog.Common.Interfaces.ViewModel
{
    using System.Collections.Generic;

    public interface IBookWithAuthor : Entity.IBook
    {
        IEnumerable<Entity.IAuthor> Authors { get; set; }
    }
}
