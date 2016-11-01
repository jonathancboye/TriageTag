using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class EditMedicalDataPage : ContentPage
    {
        App application = Application.Current as App;
        
        public EditMedicalDataPage() {
            InitializeComponent();
        }

        private void OnSaveButtonClicked( object sender, EventArgs e ) {
            //TODO Implement OnSaveButtonClicked
        }

        private void OnCancelButtonClicked( object sender, EventArgs e ) {
            application.MainPage = application.activitiesPage;
        }
    }
}
