using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Checkout
    {
        public int CheckoutId { get; set; }
        public int CopyId { get; set; }
        public int PatronId { get; set; }
        [ForeignKey("CopyId")]
        public Copy Copy { get; set; }
        [ForeignKey("PatronId")]
        public Patron Patron { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}