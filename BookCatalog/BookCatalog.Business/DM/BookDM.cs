using AutoMapper;
using BookCatalog.Data;
using BookCatalog.Data.Entity.Author;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalog.Portal.ViewModel.Book;
using BookCatalog.Portal.ViewModel.Author;
using BookCatalog.Data.Entity.Book;

namespace BookCatalog.Business.DM
{
    public class BookDM
    {
        private readonly Repository repository;
        
        public BookDM(string connString)
        {
            repository = new Repository(connString);
        }  

        public List<BookVM> GetBooksList()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<BookEM, BookVM>();
                cfg.CreateMap<AuthorVM, AuthorVM>();
            });

            var bookVMs = repository.GetBooks().Select(x => Mapper.Map<BookVM>(x)).ToList();

            GetBooksAuthors(bookVMs);

            return bookVMs;
        }

        private void GetBooksAuthors(List<BookVM> books)
        {
            var taskList = new List<Task<KeyValuePair<int, IEnumerable<AuthorEM>>>>();
            var booksAuthors = new Dictionary<int, IEnumerable<AuthorEM>>();

            foreach (int id in books.Select(x => x.Id))
            {
                taskList.Add(this.repository.GetBookAuthors(id));
            }

            var taskArray = taskList.ToArray();

            Task.WaitAll(taskArray);

            foreach (Task<KeyValuePair<int, IEnumerable<AuthorEM>>> task in taskArray)
            {
                var pair = task.Result;

                var bookIndex = books.IndexOf(books.First(x => x.Id == pair.Key));

                books[bookIndex].Authors = pair.Value.Select(x => Mapper.Map<AuthorVM>(x)).ToArray();
            }
        }
    }
}
