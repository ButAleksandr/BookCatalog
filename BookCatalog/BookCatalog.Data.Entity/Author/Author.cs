﻿namespace BookCatalog.Data.Entity.Author
{
    public class AuthorEM
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BookCount { get; set; }
    }
}
