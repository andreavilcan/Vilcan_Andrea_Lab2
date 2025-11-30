using System;
using System.IO;
using Vilcan_Andrea_Lab7.Data;

namespace Vilcan_Andrea_Lab7;

public partial class App : Application
{
    // instanța statică a bazei de date
    static ShoppingListDatabase? database;

    public static ShoppingListDatabase Database
    {
        get
        {
            if (database == null)
            {
                var path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ShoppingList.db3");

                database = new ShoppingListDatabase(path);
            }

            return database;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}