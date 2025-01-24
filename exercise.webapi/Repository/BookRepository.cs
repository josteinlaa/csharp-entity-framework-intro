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
            post.AuthorIds.ForEach(aid => {
                if (_db.Authors.Find(aid) == null)
                {
                    throw new KeyNotFoundException("Author id not valid");
                }
            });

            var book = new Book
            {
                Title = post.Title
            };

            foreach (var authorId in post.AuthorIds)
            {
                var author = await _db.Authors.FindAsync(authorId);
                if (author != null)
                {
                    book.Authors.Add(author);
                }
            }

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();

            return book;
        }
        public async Task<bool> DeleteBook(int id)
        {
            var target = await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();
            if (target == null) return false;

            _db.Books.Remove(target);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Authors).ToListAsync();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();
        }

        public async Task<Book> UpdateBook(int id, BookPut put)
        {
            var target = await _db.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id) ?? throw new KeyNotFoundException();            
            if (put.Title != null)
            {
                target.Title = put.Title;
            }

            await _db.SaveChangesAsync();
            return target;
        }
    }
}
