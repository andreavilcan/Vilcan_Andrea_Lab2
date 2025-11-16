namespace Vilcan_Andrea_Lab2.Models;
using System.Collections.Generic;
public class Category
{
public int ID { get; set; }
public string CategoryName { get; set; }
public ICollection<BookCategory>? BookCategories { get; set; }
}