using System;
using exercise.webapi.DTO;
using exercise.webapi.Models;
using exercise.webapi.Repository;

namespace exercise.webapi.Endpoints;

public static class AuthorApi
{
    public static void ConfigureAuthorApi(this WebApplication app) 
    {
        app.MapGet("/authors", GetAll);
        app.MapGet("/authors/{id}", GetAuthorById);
    }

    private static async Task<IResult> GetAll(IAuthorRepository repository)
    {
        Payload<IEnumerable<AuthorResponse>> payload = new Payload<IEnumerable<AuthorResponse>>();
        var authors = await repository.GetAllAuthors();
        var authorResponses = authors.Select(a => AuthorToReponse(a));
        payload.Data = authorResponses;

        return TypedResults.Ok(payload);
    }

    private static async Task<IResult> GetAuthorById(IAuthorRepository repository, int id)
    {
        try
            {
                var author = await repository.GetAuthor(id);
                
                return TypedResults.Ok(AuthorToReponse(author));
            }
            catch (KeyNotFoundException ex) 
            { 
                return TypedResults.NotFound(ex.Message);
            }
    }

    private static AuthorResponse AuthorToReponse(Author author)
    {
        return new AuthorResponse
        {
            Name = author.FirstName + " " + author.LastName,
            Books = author.Books.Select(b => new BookResponse
            {
                Title = b.Title
            }).ToList()
        };
    }
}
