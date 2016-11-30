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
        bool connectionMade = false;

        public LoginPage() {
            Padding = new Thickness( 5, 20, 5, 20 );
            InitializeComponent();

            if ( Device.OS == TargetPlatform.Android ) {
                updateDatabase();
            }
        }

        // Grab database file from Ftp server
        async private Task updateDatabase() {
            connectionStatus.IsVisible = true;
            bool updated = await DependencyService.Get<IFtpRequest>().FtpRequest( "ftp://jonathancboye.duckdns.org:20201/" + App.DatabaseFilename, "Triage", "1234" );

            if ( updated ) {
                connectionStatus.TextColor = Color.Green;
                connectionStatus.Text = "Successfully updated database";
            } else {
                connectionStatus.Text = "Database not updated";
            }
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

        private void OnLoginButtonClicked( object sender, EventArgs e ) {
            if ( !connectionMade ) {
                makeConnection();
            } else {
                validate();
            }
        }

        async private void makeConnection() {
            // Connect to database file
            App.dbConnection = await DependencyService.Get<ISQLite>().getConnection(App.DatabaseFilename);

            // Login failed
            if ( App.dbConnection == null ) {
                await DisplayAlert( "ERROR", "Failed to create a connection with the database", "Close" );
                return;
            }

            // Create test database
            //TestDatabase testDatabase =  new TestDatabase( App.dbConnection );

            connectionMade = true;
            validate();
        }

        async private void validate() {
            if ( checkUserPassword() ) {
                await Navigation.PushAsync( new ActivitiesPage() );
            } else {
                invalidText.IsVisible = true;
            }

            // Clear text fields
            username.Text = "";
            password.Text = "";
        }

        private bool checkUserPassword() {
            bool valid = false;
            DecryptedUser user = Database.getUser( username.Text, password.Text );
            if ( user != null ) {
                App.UID = user.employeeId;
                App.uLvl = user.userLvl;
                valid = true;
            }

            return valid;
        }
    }
}
