using System.Collections.Generic;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}