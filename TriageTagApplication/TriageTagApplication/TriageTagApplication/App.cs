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
        public SQLiteConnection dbConnection; // Database Connection
        public int UID; // Current logged in user

        public App() {
           
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
