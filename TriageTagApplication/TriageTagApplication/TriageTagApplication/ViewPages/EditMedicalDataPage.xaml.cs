using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class EditMedicalDataPage : ContentPage
    {
        DecryptedMedicalHistory mhistory;
        Editable editable;  // If true then Entrys are editable

        public EditMedicalDataPage() {
            editable = new Editable() {
                IsEditable = false
            };

            InitializeComponent();
            makeEditableGrid();
        }

        private void makeEditableGrid() {
            int numberOfLables = 6; // Number of columns in the Medical History Table

            // Check for medical history
            mhistory = Database.getMedicalHistory( App.UID );
            if ( mhistory == null ) {
                // No medical history found
                System.Diagnostics.Debug.WriteLine( "No medical history" );
                return;
            }

            Grid grid = new Grid();

            // Create a row definition for each field
            for ( int i = 0; i < numberOfLables; i++ ) {
                grid.RowDefinitions.Add( new RowDefinition() { Height = new GridLength( 0.5, GridUnitType.Star ) } );
            }

            // column definition for name of medical history
            grid.ColumnDefinitions.Add( new ColumnDefinition() { Width = new GridLength( 1, GridUnitType.Star ) } );
            // column definition for value of medical history 
            grid.ColumnDefinitions.Add( new ColumnDefinition() { Width = new GridLength( 3, GridUnitType.Star ) } );

            // Add rows to grid
            int rownumber = 0;
            addGridRow( ref grid, "Allergies", "allergies", rownumber++ );
            addGridRow( ref grid, "Blood Type", "bloodType", rownumber++ );
            addGridRow( ref grid, "Religion", "religion", rownumber++ );
            addGridRow( ref grid, "High Blood Pressure", "highBloodPressure", rownumber++ );
            addGridRow( ref grid, "Medications", "medications", rownumber++ );
            addGridRow( ref grid, "Primary Doctor", "primaryDoctor", rownumber++ );

            editButton.IsEnabled = true;
            scrollView.Content = grid;
        }

        // Places Lables and Entrys into grid
        private void addGridRow( ref Grid grid, string labelText, string binding, int rownumber ) {
            // Alternate row colors in grid
            Color bgColor;
            if ( rownumber % 2 == 0 ) {
                bgColor = Color.FromHex( "#D7CEC7" );
            } else {
                bgColor = Color.FromHex( "#D7CEC7" );
            }

            // add label to grid
            Label label = new Label { Text = labelText };
            label.BackgroundColor = bgColor;
            label.HorizontalTextAlignment = TextAlignment.Center;
            label.VerticalTextAlignment = TextAlignment.Center;
            Grid.SetRow( label, rownumber );
            Grid.SetColumn( label, 0 );
            grid.Children.Add( label );

            //add entry to grid
            Entry entry = new Entry();
            entry.HorizontalTextAlignment = TextAlignment.Center;
            entry.SetBinding( Entry.TextProperty, new Binding {
                Source = mhistory,
                Path = binding,
                Mode = BindingMode.TwoWay
            } );
            entry.SetBinding( Entry.IsEnabledProperty, new Binding {
                Source = editable,
                Path = "IsEditable",
                Mode = BindingMode.OneWay
            } );
            Grid.SetRow( entry, rownumber );
            Grid.SetColumn( entry, 1 );
            grid.Children.Add( entry );
        }

        async private void OnSaveButtonClicked( object sender, EventArgs e ) {
            // Update the database
            Database.updateMedicalHistory( mhistory );
            editable.IsEditable = false;

            // Update the Ftp database file
            await DependencyService.Get<ISQLite>().copyFileToFtpServer( App.DatabaseFilename );

        }

        private void OnEditButtonClicked( object sender, EventArgs e ) {
            editable.IsEditable = true;
        }

        async private void OnbackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }
    }
}
