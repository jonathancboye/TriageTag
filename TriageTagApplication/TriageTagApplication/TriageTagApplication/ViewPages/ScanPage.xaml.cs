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
        App app = Application.Current as App;
        public Button readButton;
        public Button writeButton;

        public ScanPage() {
            InitializeComponent();

            readButton = new Button {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Read Tag"
            };

            writeButton = new Button {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Write Tag"
            };

            Button cancelButton = new Button {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Cancel"
            };
            cancelButton.Clicked += OnCancelButtonClicked;

            // Only Administrators can write to tags
            if(app.uLvl != 2 ) {
                writeButton.IsVisible = false;
            }

            layout.Children.Add( readButton );
            layout.Children.Add( writeButton );
            layout.Children.Add( cancelButton );
        }

        async private void OnCancelButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }

        public void OnWriteButtonClicked( string message ) {
            System.Diagnostics.Debug.WriteLine( message );
        }

        async public void OnReadButtonClicked( string employeeId ) {
            await Navigation.PushAsync( new DisplayMedicalDataPage(employeeId));
        }
    }
}
