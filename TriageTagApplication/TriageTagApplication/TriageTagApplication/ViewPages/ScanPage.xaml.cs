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
        public Button readButton;
        public Button writeButton;
        private Entry entry;
        private Label label;
        public ScanPage() {
            InitializeComponent();

            label = new Label {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "Read a Tag"
            };

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

            entry = new Entry {
                IsEnabled = true,
                IsVisible = true,
                Placeholder = "Message to write",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            layout.Children.Add( label );
            layout.Children.Add( readButton );
            layout.Children.Add( entry );
            layout.Children.Add( writeButton );
            layout.Children.Add( cancelButton );
        }

        async private void OnCancelButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }

        public void OnWriteButtonClicked( string message ) {
            System.Diagnostics.Debug.WriteLine( message );
        }

        public void OnReadButtonClicked( string message ) {
            label.Text = message;
        }

        public string getMessageToWrite() {
            return entry.Text;
        }
    }
}
