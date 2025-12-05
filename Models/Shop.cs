using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Vilcan_Andrea_Lab7.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ShopName { get; set; }

        public string Adress { get; set; }

        // pentru afișarea în Picker
        public string ShopDetails => ShopName + " \n" + Adress;

        [OneToMany]
        public List<ShopList> ShopLists { get; set; }
    }
}