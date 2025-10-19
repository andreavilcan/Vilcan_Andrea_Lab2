using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vilcan_Andrea_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name ="Book Title")]
        public string Title { get; set; }
        public string Author { get; set; }

        [Column(TypeName = "numeric(6, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }   // ← tip corect
    }
}