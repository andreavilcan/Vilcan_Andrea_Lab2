using SQLite;
using SQLiteNetExtensions.Attributes;
namespace Vilcan_Andrea_Lab7.Models;
using SQLiteNetExtensions.Attributes;

public class ShopList
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [MaxLength(250), Unique]
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    [ForeignKey(typeof(Shop))]
    public int ShopID { get; set; }
}