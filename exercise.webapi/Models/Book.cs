using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.webapi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BookAuthor> BookAuthor { get; set; } = new List<BookAuthor>();
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
