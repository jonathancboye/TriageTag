using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class DisplayMedicalDatapage : ContentPage
    {
        App application = Application.Current as App;

        public DisplayMedicalDatapage() {
            InitializeComponent();
        }

        async private void OnBackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();    
        }
    }
}
