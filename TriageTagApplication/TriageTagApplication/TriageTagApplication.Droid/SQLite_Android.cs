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
#pragma warning disable CS1998 
[assembly: Dependency ( typeof( SQLite_Android ) )]
namespace TriageTagApplication.Droid
{
    class SQLite_Android: ISQLite
    {
        public SQLite_Android() { }

        // Returns: SQLiteConnection from a database file
        async public Task<SQLiteConnection> getConnection() {
            //File IO on Android is done with System.IO API

            //This is an example of how one might creat a connection to a database file
            string sqliteFilename = "database.db3";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = Path.Combine( folderPath, sqliteFilename ); // Documents folder

            // Check if database file exists
            if ( !File.Exists( filePath ) ) {
                // TODO: Figure out what to do if database file is not found
            }

            //Create SQLiteConnection
            SQLiteConnection connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), filePath);

            return connection;
        }

        // TODO: Does nothing. Need to restructure code should not be in this class
        async public Task copyFileToFtpServer(){ }
    }
}