using System.Collections.Generic;
using Vilcan_Andrea_Lab2.Models;

namespace Vilcan_Andrea_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}