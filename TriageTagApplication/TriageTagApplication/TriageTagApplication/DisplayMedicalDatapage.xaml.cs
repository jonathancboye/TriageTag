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

        private void OnBackButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.activitiesPage;    
        }
    }
}
