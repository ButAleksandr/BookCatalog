using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Portal.ViewModel.Book
{
    public class BookVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public string PageCount { get; set; }

        [Required]
        public System.DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        public IEnumerable<AuthorVM> Authors { get; set; }
    }
}
