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

            // Reset Entry fields
            username.Text = "";
            password.Text = "";
        }

        async private void makeConnection() {
            // Connect to database file
            app.dbConnection = await DependencyService.Get<ISQLite>().getConnection();

            // Create test database
            TestDatabase testDatabase =  new TestDatabase( app.dbConnection );

            connectionMade = true;

            validate();
        }

        private void validate() {
            // Query database for user
            List<Users> users = app.dbConnection.Query<Users>( "SELECT * FROM Users WHERE username=? AND password=?", username.Text, password.Text);
            if ( users.Count == 1 ) {
                // Set UID so we know who the which user logged
                app.UID = users[0].employeeId;
                app.MainPage = app.activitiesPage;
                invalidText.IsVisible = false;
            } else {
                invalidText.IsVisible = true;
            }
        }
    }
}
