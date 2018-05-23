namespace BookCatalog.Data.Entity.Book
{
    public class BookEM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PageCount { get; set; }

        public System.DateTime ReleaseDate { get; set; }

        public int Rate { get; set; }
    }
}
