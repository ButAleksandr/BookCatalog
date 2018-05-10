namespace BookCatalog.Common.Interfaces.Entity
{
    public interface IBook
    {
        int Id { get; set; }

        string Name { get; set; }

        string PageCount { get; set; }

        System.DateTime ReleaseDate { get; set; }

        int Rate { get; set; }
    }
}
