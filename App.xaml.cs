using System.IO;
using Microsoft.Maui.Storage;
using Vilcan_Andrea_Lab7.Data;

namespace Vilcan_Andrea_Lab7;

public partial class App : Application
{
    private static ShoppingListDatabase? database;

    public static ShoppingListDatabase Database
    {
        get
        {
            if (database == null)
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "ShoppingList.db3");
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