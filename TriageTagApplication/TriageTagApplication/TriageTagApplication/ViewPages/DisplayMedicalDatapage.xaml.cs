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
        DecryptedEmployee employee;
        string _employeeId;

        public DisplayMedicalDataPage(string employeeId) {
            _employeeId = employeeId;
            InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            int numberOfLables = 11; // Number of columns in the Medical History Table

            mhistory = Database.getMedicalHistory( _employeeId );
            employee = Database.getEmployee( _employeeId );
            if (mhistory == null || employee == null) {
                // No medical history found
                System.Diagnostics.Debug.WriteLine( "No medical history or employee" );
                return;
            }

            Grid grid = new Grid();
            grid.RowSpacing = 0;
            grid.BackgroundColor = Color.Black;

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
            addGridRow( ref grid, "employee", "First Name", "firstname", rownumber++ );
            addGridRow( ref grid, "employee", "Last Name", "lastname", rownumber++ );
            addGridRow( ref grid, "employee", "Address", "address", rownumber++ );
            addGridRow( ref grid, "employee", "Phone", "phonenumber", rownumber++ );
            addGridRow( ref grid, "employee", "Emergency Contact", "emergencyContact", rownumber++ );
            addGridRow( ref grid, "medical", "Allergies", "allergies", rownumber++ );
            addGridRow( ref grid, "medical", "Blood Type", "bloodType", rownumber++ );
            addGridRow( ref grid, "medical", "Religion", "religion", rownumber++ );
            addGridRow( ref grid, "medical", "High Blood Pressure", "highBloodPressure", rownumber++ );
            addGridRow( ref grid, "medical", "Medications", "medications", rownumber++ );
            addGridRow( ref grid, "medical", "Primary Doctor", "primaryDoctor", rownumber++ );

            scrollView.Content = grid;
        }

        // Places Lables and Entrys into grid
        private void addGridRow( ref Grid grid, string type, string labelText, string binding, int rownumber ) {
            // Alternate row colors in grid
            Color bgColor;
            if ( rownumber % 2 == 0 ) {
                bgColor = Color.FromHex( "#D7CEC7" );
            } else {
                bgColor = Color.FromHex( "#DAF7A6" );
            }

            // add label description to grid
            Label description = new Label { Text = labelText };
            BoxView bview = new BoxView {
                BackgroundColor = bgColor,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            description.BackgroundColor = bgColor;
            description.HorizontalTextAlignment = TextAlignment.Center;
            description.VerticalTextAlignment = TextAlignment.Center;
            description.HorizontalOptions = LayoutOptions.Fill;
            description.VerticalOptions = LayoutOptions.Fill;
            Grid.SetRow( bview, rownumber );
            Grid.SetRow( description, rownumber );
            Grid.SetColumn( bview, 0 );        
            Grid.SetColumn( description, 0 );
            grid.Children.Add( bview );
            grid.Children.Add( description );

            //add label value to grid
            Label value = new Label();
            value.BackgroundColor = bgColor;
            value.HorizontalTextAlignment = TextAlignment.Center;
            if(type == "medical" ) {
                value.SetBinding( Label.TextProperty, new Binding {
                    Source = mhistory,
                    Path = binding,
                    Mode = BindingMode.OneWay
                } );
            }else if(type == "employee" ) {
                value.SetBinding( Label.TextProperty, new Binding {
                    Source = employee,
                    Path = binding,
                    Mode = BindingMode.OneWay
                } );
            }
            Grid.SetRow( bview, rownumber );
            Grid.SetRow( value, rownumber );
            Grid.SetColumn( bview, 1 );
            Grid.SetColumn( value, 1 );
            grid.Children.Add( bview );
            grid.Children.Add( value );
        }


        async private void OnBackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();    
        }
    }
}
