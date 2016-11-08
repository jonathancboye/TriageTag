using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteExample.UWP;
using Xamarin.Forms;
using SQLite.Net;
using Windows.Storage;

[assembly: Dependency( typeof( SQLite_UWP ) )] 
namespace SQLiteExample.UWP
{
    class SQLite_UWP: ISQLite
    {
        async public Task<SQLiteConnection> getConnection() {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile databaseFile = await storageFolder.CreateFileAsync("database.db3");
            SQLiteConnection conn =
                new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(),
                databaseFile.Path);
            return conn;
        }
    }
}
