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

        async private void OnLogoutButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }

        async private void OnScanTagButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new ScanPage() );
        }

        async private void OnEditMedicalDataButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new EditMedicalDataPage() );
        }

        async private void OnSynchronizeButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new SynchronizingPage() );
        }
    }
}
