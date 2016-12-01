using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class DisplayMedicalDataPage : ContentPage
    {
        DecryptedMedicalHistory mhistory;
        string _employeeId;

        public DisplayMedicalDataPage(string employeeId) {
            _employeeId = employeeId;
            InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            int numberOfLables = 6; // Number of columns in the Medical History Table

            mhistory = Database.getMedicalHistory( _employeeId );
            if (mhistory == null) {
                // No medical history found
                System.Diagnostics.Debug.WriteLine( "No medical history" );
                return;
            }

            Grid grid = new Grid();

            // Create a row definition for each field
            for ( int i = 0; i < numberOfLables; i++ ) {
                grid.RowDefinitions.Add( new RowDefinition() { Height = new GridLength( 1, GridUnitType.Auto ) } );
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

            scrollView.Content = grid;
        }

        // Places Lables and Entrys into grid
        private void addGridRow( ref Grid grid, string labelText, string binding, int rownumber ) {
            // Alternate row colors in grid
            Color bgColor;
            if ( rownumber % 2 == 0 ) {
                bgColor = Color.Fuchsia;
            } else {
                bgColor = Color.Aqua;
            }

            // add label description to grid
            Label description = new Label { Text = labelText };
            description.BackgroundColor = bgColor;
            description.HorizontalTextAlignment = TextAlignment.Center;
            description.VerticalTextAlignment = TextAlignment.Center;
            Grid.SetRow( description, rownumber );
            Grid.SetColumn( description, 0 );
            grid.Children.Add( description );

            //add label value to grid
            Label value = new Label();
            value.BackgroundColor = bgColor;
            value.HorizontalTextAlignment = TextAlignment.Center;
            value.SetBinding( Label.TextProperty, new Binding {
                Source = mhistory,
                Path = binding,
                Mode = BindingMode.OneWay
            } );          
            Grid.SetRow( value, rownumber );
            Grid.SetColumn( value, 1 );
            grid.Children.Add( value );
        }


        async private void OnBackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();    
        }
    }
}
