using exercise.webapi.DTO;
using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBook(int id);
        public Task<Book> AddBook(BookPost post);
        public Task<Book> UpdateBook(int id, BookPut put);
        public Task<bool> DeleteBook(int id);
    }
}
