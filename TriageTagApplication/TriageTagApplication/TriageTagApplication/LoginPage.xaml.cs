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
        App application = Application.Current as App;

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

        private void OnButtonClicked( object sender, EventArgs e ) {
            SQLiteConnection connection = DependencyService.Get<ISQLite>().GetConnection();
            Debug.WriteLine("Hello");
            application.MainPage = application.activitiesPage;
        }
    }
}
 