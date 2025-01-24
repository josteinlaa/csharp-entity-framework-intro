using System;
using System.Collections.Generic;

namespace exercise.webapi.DTO;

public class BookAuthorResponse
{
    public string Title { get; set; }
    public IEnumerable<string> AuthorNames { get; set; }
}
