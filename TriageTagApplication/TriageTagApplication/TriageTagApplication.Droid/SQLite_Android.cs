using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
using Xamarin.Android;

[assembly: Dependency ( typeof( SQLite_Android ) )]
namespace TriageTagApplication.Droid
{
    class SQLite_Android: ISQLite
    {
        public SQLite_Android() { }

        async public Task<SQLiteConnection> getConnection() {
            //This is an example of how one would might creat a connection to a database file
            string sqliteFilename = "database.db3";
            string filePath = Path.Combine( System.Environment.CurrentDirectory, sqliteFilename ); // Documents folder
            
            //if( !File.Exists( filePath ) ) {
                FileStream fs = File.Create( filePath );
                fs.Close();
            // }

            SQLiteConnection connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), filePath);
            return connection;
        }
    }
}