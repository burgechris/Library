using System.Collections.Generic;
using System;

namespace Library.Models
{
    public class Patron
    {
        public Patron()
        {
            this.Rentals = new HashSet<Checkout>();
        }

        public int PatronId { get; set; }
        public int PatronName { get; set; }
        //history
        public ICollection<Checkout> Rentals { get; }
    }
}