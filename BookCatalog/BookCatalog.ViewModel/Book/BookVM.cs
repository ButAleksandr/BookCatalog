using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;

namespace BookCatalog.Portal.ViewModel.Book
{
    public class BookVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PageCount { get; set; }

        public System.DateTime ReleaseDate { get; set; }

        public int Rate { get; set; }

        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}
