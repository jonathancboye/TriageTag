﻿using System;
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
        public SQLite_UWP() { }

        async public Task<SQLiteConnection> getConnection() {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile databaseFile = await localFolder.CreateFileAsync("database.db3", CreationCollisionOption.ReplaceExisting);
            SQLiteConnection connection = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFile.Path);

            System.Diagnostics.Debug.WriteLine( databaseFile.Path );
            return connection;
        }
    }
}