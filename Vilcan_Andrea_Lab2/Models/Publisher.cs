namespace Vilcan_Andrea_Lab2.Models;
using System.Collections.Generic;
public class Publisher
{
    public int ID { get; set; }
    public string PublisherName { get; set; }
    public ICollection<Book>? Books { get; set; } //navigation property
}