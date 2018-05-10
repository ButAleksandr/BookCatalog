namespace BookCatalog.Data.Entity.Book
{
    public class Book : Common.Interfaces.Entity.IBook
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PageCount { get; set; }

        public System.DateTime ReleaseDate { get; set; }

        public int Rate { get; set; }
    }
}
