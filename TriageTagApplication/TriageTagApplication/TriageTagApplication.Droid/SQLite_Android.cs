using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite.Net;
using SQLite.Net.Interop;
using TriageTagApplication.Droid;
using Xamarin.Forms;

[assembly: Dependency ( typeof( SQLite_Android ) )]
namespace TriageTagApplication.Droid
{
    class SQLite_Android: ISQLite
    {
        public SQLite_Android() { }

        public SQLiteConnection GetConnection() {
            //This is an example of how one would might creat a connection to a database file
            string sqliteFilename = "users.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            string path = Path.Combine(documentsPath, sqliteFilename);
            SQLiteConnection connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);
            return connection;
        }
    }
}