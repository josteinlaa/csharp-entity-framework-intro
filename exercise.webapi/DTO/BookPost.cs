using System;

namespace exercise.webapi.DTO;

public class BookPost
{
    public string Title { get; set; }
    public List<int> AuthorIds { get; set; } = [];
}
