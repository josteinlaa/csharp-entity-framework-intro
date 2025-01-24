using exercise.webapi.DTO;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            app.MapGet("/books", GetAll);
            app.MapGet("/books/{id}", GetBookById);
            app.MapPost("/books", AddBook);
            app.MapPut("/books/{id}", UpdateBook);
            app.MapDelete("/books/{id}", DeleteBook);
        }

        private static async Task<IResult> DeleteBook(IBookRepository repository, int id)
        {
            var deleted = await repository.DeleteBook(id);
            if (deleted)
            {
                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.NotFound();
        }
        }

        private static async Task<IResult> UpdateBook(IBookRepository repository, int id, BookPut put)
        {
            try
            {
                var updatedBook = await repository.UpdateBook(id, put);
                return TypedResults.Created($"Updated {updatedBook.Id}", BookToResponse(updatedBook));
            }
            catch (KeyNotFoundException ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        private static async Task<IResult> AddBook(IBookRepository repository, BookPost post)
        {
            try
            {
                var addedBook = await repository.AddBook(post);
                return TypedResults.Created($"Added {addedBook.Id}", BookToResponse(addedBook));
            }
            catch (KeyNotFoundException ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        private static async Task<IResult> GetBookById(IBookRepository repository, int id)
        {
            try
            {
                var book = await repository.GetBook(id);
                
                return TypedResults.Ok(BookToResponse(book));
            }
            catch (KeyNotFoundException ex) 
            { 
                return TypedResults.NotFound(ex.Message);
            }
        }

        private static async Task<IResult> GetAll(IBookRepository bookRepository)
        {
            Payload<IEnumerable<BookAuthorResponse>> payload = new Payload<IEnumerable<BookAuthorResponse>>();
            
            var books = await bookRepository.GetAllBooks();
            var bookAuthorResponses = books.Select(BookToResponse);
            payload.Data = bookAuthorResponses;
            
            return TypedResults.Ok(payload);
        }

        private static BookAuthorResponse BookToResponse(Book book)
        {
            return new BookAuthorResponse
            {
                Title = book.Title,
                AuthorNames = book.Authors.Select(author => author.FirstName + " " + author.LastName)
            };
        }
    }
}
