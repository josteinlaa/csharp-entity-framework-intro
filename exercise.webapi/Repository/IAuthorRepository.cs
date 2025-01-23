using System;
using exercise.webapi.Models;

namespace exercise.webapi.Repository;

public interface IAuthorRepository
{
    Task<Author> GetAuthor(int id);
    Task<IEnumerable<Author>> GetAllAuthors();
}
