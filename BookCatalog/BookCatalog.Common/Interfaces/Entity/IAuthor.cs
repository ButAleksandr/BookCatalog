namespace BookCatalog.Common.Interfaces.Entity
{
    public interface IAuthor
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        int BookCount { get; set; }
    }
}
