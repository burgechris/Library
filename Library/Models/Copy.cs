using System.Collections.Generic;
using System;

namespace Library.Models
{
    public class Copy : Book
    {
        public Copy()
        {
            this.Patrons = new HashSet<Checkout>();
        }

        public int CopyId { get; set; }
        public int CopyAmnt { get; set; }

        public ICollection<Checkout> Patrons { get; }
    }
}