using AutoMapper;
using BookCatalog.Common.Business;
using BookCatalog.Common.Data;
using BookCatalog.Data.Entity.Author;
using BookCatalog.Portal.ViewModel.Author;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalog.Business.DM
{
    public class AuthorDM : IAuthorDM
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

        public AuthorVM Get(int author)
        {
            return Mapper.Map<AuthorVM>(repository.Get(author));
        }

        public void UpdateBookAuthors(int bookId, IEnumerable<int> authorIds)
        {
            repository.UpdateBookAuthors(bookId, authorIds);
        }

        public AuthorVM Save(AuthorVM authorVM)
        {
            var authorEM = Mapper.Map<AuthorEM>(authorVM);

            if (repository.AuthorIsExist(authorEM))
            {
                authorEM = repository.Update(authorEM);
            }
            else
            {
                authorEM = repository.Save(authorEM);
            }

            return Mapper.Map<AuthorVM>(authorEM);
        }

        public void DeleteAuthor(int bookId)
        {
            repository.DeleteAuthor(bookId);
        }
    }
}
