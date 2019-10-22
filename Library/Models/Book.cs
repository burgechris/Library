using System.Collections.Generic;
using System;

namespace Library.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<AuthorBook>();
        }

        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }

        public ICollection<AuthorBook> Authors { get; }
    }
}