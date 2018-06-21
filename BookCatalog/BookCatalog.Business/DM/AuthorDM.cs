using AutoMapper;
using BookCatalog.Common.Business;
using BookCatalog.Common.Data;
using BookCatalog.Data.Entity.Author;
using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalog.Business.DM
{
    public class AuthorDM: IAuthorDM
    {
        private readonly IAuthorRepository repository;

        public AuthorDM(IAuthorRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<AuthorVM> GetAll()
        {
            return repository.GetAll().Select(x => Mapper.Map<AuthorVM>(x));
        }

        public void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds)
        {
            repository.UpdateBookAuthors(bookId, authorIds);
        }
    }
}
