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

using TriageTagApplication;

using SQLite.Net;

using Xamarin.Forms;

//[assembly: Dependency ( typeof( SQLite_Android ) )]
namespace TriageTagApplication.Droid
{
    class SQLite_Android: ISQLite
    {
        public SQLite_Android() { }

        public SQLiteConnection GetConnection() {
            //var sqliteFilename = "users.db3";
            //string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
            //var path = Path.Combine(documentsPath, sqliteFilename);
            //// Create the connection
            //var conn = new SQLiteConnection(SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path);
            //// Return the database connection
            //return conn;
            return null;
        }
    }
}