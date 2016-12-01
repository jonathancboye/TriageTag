using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class SynchronizingPage : ContentPage
    {
        App application = Application.Current as App;

        public SynchronizingPage() {
            InitializeComponent();
        }

        async private void OnCancelButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }

        private void OnSyncButtonClicked( object sender, EventArgs e ) {
            DependencyService.Get<IFtpRequest>().FtpRequest( "ftp://jonathancboye.duckdns.org:20201/" + App.DatabaseFilename, "Triage", "1234" );
        }
    }
}
