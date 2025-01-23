using exercise.webapi.Data;
using exercise.webapi.DTO;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository: IBookRepository
    {
        DataContext _db;
        
        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<Book> AddBook(BookPost post)
        {
            if (_db.Authors.Find(post.AuthorId) == null) throw new KeyNotFoundException("Author id not valid");

            var book = new Book
            {
                Title = post.Title,
                AuthorId = post.AuthorId
            };

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return book;
        }
        public async Task<bool> DeleteBook(int id)
        {
            var target = await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();
            if (target == null) return false;

            _db.Books.Remove(target);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();

        }

        public async Task<Book> GetBook(int id)
        {
            return await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> UpdateBook(int id, BookPut put)
        {
            var target = await _db.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();
            if (put.AuthorId != null && _db.Authors.Find(put.AuthorId) == null) throw new KeyNotFoundException("Author id not valid");
            
            if (put.Title != null)
            {
                target.Title = put.Title;
            }
            if (put.AuthorId != null)
            {
                target.AuthorId = (int)put.AuthorId;
            }

            await _db.SaveChangesAsync();
            return target;
        }
    }
}
