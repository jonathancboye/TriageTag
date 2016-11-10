using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TriageTagApplication.Model;
using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class EditMedicalDataPage : ContentPage
    {
        App app = Application.Current as App;
        MedicalHistory mhistory;
        Editable editable;  // If true then Entrys are editable

        public EditMedicalDataPage() {
            editable = new Editable() {
                IsEditable = false
            };

           
            InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            int numberOfLables = 6; // Number of columns in the Medical History Table

            // Check for medical history
            List<MedicalHistory> mhistorys = app.dbConnection.Query<MedicalHistory>( "SELECT * FROM MedicalHistory WHERE employeeId=?", app.UID );
            if ( mhistorys.Count > 0 ) {
                mhistory = mhistorys[0];
            } else {
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
            Color bgColor;

            if ( rownumber % 2 == 0 ) {
                bgColor = Color.Fuchsia;
            } else {
                bgColor = Color.Aqua;
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

        private void OnSaveButtonClicked( object sender, EventArgs e ) {
            app.dbConnection.Update( mhistory );
            editable.IsEditable = false;
        }

        private void OnEditButtonClicked( object sender, EventArgs e ) {
            editable.IsEditable = true;
        }

        async private void OnbackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }
    }
}
