using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public string Edition { get; set; }

        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public List<BookCategory> BookCategories { get; set; }
    }
}
