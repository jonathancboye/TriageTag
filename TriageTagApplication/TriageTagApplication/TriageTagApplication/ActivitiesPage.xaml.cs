using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class ActivitiesPage : ContentPage
    {
        App application = Application.Current as App;

        public ActivitiesPage() {
            InitializeComponent();
        }

        private void OnLogoutButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.loginPage;
        }

        private void OnScanTagButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.scanPage;
        }

        private void OnEditMedicalDataButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.editMedicalDataPage;
        }

        private void OnSynchronizeButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.synchornizingPage;
        }
    }
}
