namespace BookCatalog.Data.Entity.Author
{
    public class Author : Common.Interfaces.Entity.IAuthor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BookCount { get; set; }
    }
}
