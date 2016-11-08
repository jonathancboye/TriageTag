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

        public LoginPage() {
            Padding = new Thickness( 5, 20, 5, 20 );
            InitializeComponent();
        }

        private void OnTextChanged( object sender, TextChangedEventArgs e ) {
           Entry entry = (Entry)sender;

           if(username.Text != "" && password.Text != "" ) {
                loginButton.IsEnabled = true;
            }
            else {
                loginButton.IsEnabled = false;
            }

        }

        async private void OnButtonClicked( object sender, EventArgs e ) {
            app.dbConnection = await DependencyService.Get<ISQLite>().getConnection();
            TestDatabase testDatabase =  new TestDatabase( app.dbConnection );
            List<Users> users = app.dbConnection.Query<Users>( "SELECT * FROM Users WHERE username=? AND password=?", username.Text, password.Text);
            if(users.Count == 1 ) {
                app.UID = users[0].employeeId;
                app.MainPage = app.activitiesPage;
            }
        }
    }
}
 