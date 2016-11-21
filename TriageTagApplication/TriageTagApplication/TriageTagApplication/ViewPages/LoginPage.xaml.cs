using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using SQLite.Net;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class LoginPage : ContentPage
    {
        App app = Application.Current as App;
        bool connectionMade = false;

        public LoginPage() {
            Padding = new Thickness( 5, 20, 5, 20 );
            InitializeComponent();
            
        }

       
        private void intializeSalt()
        {
            List<Salt> salts = app.dbConnection.Query<Salt>("SELECT * FROM Salt WHERE Id = 1");
            app.salt = salts[0].key;
        }

        /*
         Check if the user has entered text into the fields 
         if text is present: enable login button
         else button is disabled.
             */
        private void OnTextChanged( object sender, TextChangedEventArgs e ) {
            Entry entry = (Entry)sender;

            if ( username.Text != "" && password.Text != "" ) {
                loginButton.IsEnabled = true;
            } else {
                loginButton.IsEnabled = false;
            }
        }

        private void OnButtonClicked( object sender, EventArgs e ) {
           
            if ( !connectionMade ) {
                makeConnection();
            } else {
                validate();
            }
        }

        async private void makeConnection() {
            // Connect to database file
            app.dbConnection = await DependencyService.Get<ISQLite>().getConnection();
            
            // Create test database
            //TestDatabase testDatabase =  new TestDatabase( app.dbConnection );
            connectionMade = true;

            validate(); 
        }

        async private void validate() {

            List<Users> users = app.dbConnection.Query<Users>("SELECT * FROM Users WHERE username=? AND password=?", username.Text, password.Text);
            intializeSalt();
            if ( users.Count == 1 || checkUserPassword()) {
                await Navigation.PushAsync( new ActivitiesPage() );
            } else {
                invalidText.IsVisible = true;
            }

            // Clear text fields
            username.Text = "";
            password.Text = "";
           
        }

        private Boolean checkUserPassword()
        {
            Boolean valid = false;
            byte[] uName = Crypto.EncryptAes(username.Text,App.pkey,app.salt);
            byte[] pValue = Crypto.EncryptAes(password.Text, App.pkey, app.salt);
            List<Users> users = app.dbConnection.Query<Users>("SELECT * FROM Users WHERE username=? AND password=?", uName, pValue);
           

            if (users.Count == 1)
            {
                valid = true;

            }

            return valid;
        }


    }
}
