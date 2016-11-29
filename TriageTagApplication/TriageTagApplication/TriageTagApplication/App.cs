﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public class App: Application
    {
        public static SQLiteConnection dbConnection; // Database Connection
        public static string UID; // Current logged in user
        public static string uLvl;
        public static byte[] salt = new byte[] {0x20, 0x21, 0x69, 0x72, 0x89, 0x12, 0x57, 0x00, 0x12, 0x23 };
        public const string pkey = "btggX!AFnvAOEe7P";
        public const string DatabaseFilename = "test.db3";
       


        public App() {         
            if ( Device.OS == TargetPlatform.Android ) {
                // Grab database file from Ftp server
                DependencyService.Get<IFtpRequest>().FtpRequest( "ftp://jonathancboye.duckdns.org:20201/" + App.DatabaseFilename, "Triage", "1234" );
            }

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
