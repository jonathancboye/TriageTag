using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Callbacks.UWP;
using SQLite.Net;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency( typeof( SQLite_UWP ) )]
namespace Callbacks.UWP
{
    class SQLite_UWP: ISQLite
    {
        public SQLite_UWP() { }

        async public Task<SQLiteConnection> GetConnection() {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile dbFile = await storageFolder.CreateFileAsync("database.db3",
                CreationCollisionOption.OpenIfExists);
            System.Diagnostics.Debug.WriteLine( dbFile.Path );
            SQLiteConnection conn = new SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), dbFile.Path);
            return conn;
        }
    }
}
