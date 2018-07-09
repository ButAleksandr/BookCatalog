using System;
using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Portal.ViewModel.Book
{
    public class BookVM
    {
        public BookVM()
        {
            Id = 0;
            Name = string.Empty;
            PageCount = 0;
            Rate = 0;
            ReleaseDate = DateTime.Now;
            Authors = new List<AuthorVM>();
            AllAuthors = new List<AuthorVM>();
            AuthorIds = new List<int>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PageCount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        public IEnumerable<AuthorVM> Authors { get; set; }

        public IEnumerable<int> AuthorIds { get; set; }

        public IEnumerable<AuthorVM> AllAuthors { get; set; }
    }
}
