using System;
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
    }
}
