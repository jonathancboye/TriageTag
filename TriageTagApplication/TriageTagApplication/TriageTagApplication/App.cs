using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public class App : Application
    {
        public LoginPage loginPage;
        public ActivitiesPage activitiesPage;
        public SynchronizingPage synchornizingPage;

        public App() {
            loginPage = new LoginPage();
            activitiesPage = new ActivitiesPage();
            synchornizingPage = new SynchronizingPage();
             
            MainPage = new LoginPage();
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
