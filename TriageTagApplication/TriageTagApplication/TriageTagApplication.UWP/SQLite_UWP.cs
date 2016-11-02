using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Storage;
using SQLite.Net;
using TriageTagApplication.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_UWP))]
namespace TriageTagApplication.UWP
{
    class SQLite_UWP : ISQLite
    {
        public SQLite_UWP() { }

        public SQLiteConnection GetConnection()
        {
            //This is an example of how one would might creat a connection to a database file
            string sqliteFilename = "users.db3";
            string path = Path.Combine( KnownFolders.DocumentsLibrary.Path, sqliteFilename );
            SQLiteConnection connection = new SQLiteConnection( new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path );
            return connection;
        }
    }
}
