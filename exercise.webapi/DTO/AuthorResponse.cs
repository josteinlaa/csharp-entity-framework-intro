using System;
using exercise.webapi.Models;

namespace exercise.webapi.DTO;

public class AuthorResponse
{
    public string Name { get; set; }
    public List<BookResponse> Books { get; set; } = [];
}
