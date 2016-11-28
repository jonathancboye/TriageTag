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

[assembly: Dependency( typeof( SQLite_UWP ) )]
namespace TriageTagApplication.UWP
{
    class SQLite_UWP: ISQLite
    {

        // Must have an empty constructor
        public SQLite_UWP() { }

        // Returns: SQLiteConnection from a database file
        async public Task<SQLiteConnection> getConnection() {
            // File IO in UWP app is done with Windows.Storage API

            // Folder to store database for application to access
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            try {
                // Folder where ftp database is located
                StorageFolder ftpDatabaseFolder = await KnownFolders.DocumentsLibrary.GetFolderAsync("FTP");
                // FTP database file
                StorageFile ftpDatabaseFile = await ftpDatabaseFolder.GetFileAsync("database.db3");

                // Copy database file from FTP server to local application folder
                StorageFile databaseFile = await ftpDatabaseFile.CopyAsync( localFolder, "database.db3", NameCollisionOption.ReplaceExisting );

                // Create SQLiteConnection
                SQLiteConnection connection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFile.Path);
                System.Diagnostics.Debug.WriteLine( databaseFile.Path );
                return connection;
            } catch ( Exception exception ) {
                System.Diagnostics.Debug.WriteLine( String.Format( "Error fetching FTP database file: {0}", exception ) );               
            }

            return null;
        }
    }
}
