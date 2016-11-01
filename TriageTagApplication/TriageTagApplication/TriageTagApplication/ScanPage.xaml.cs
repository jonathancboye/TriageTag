using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class ScanPage : ContentPage
    {
        App application = Application.Current as App;

        public ScanPage() {
            InitializeComponent();
        }

        private void OnCancelButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.activitiesPage;
        }
    }
}
