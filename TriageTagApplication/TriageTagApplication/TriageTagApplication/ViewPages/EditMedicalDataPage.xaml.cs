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
        App app = Application.Current as App;
        MedicalHistory mhistory;

        public bool canEdit { get; set; }

        public EditMedicalDataPage() {
            InitializeComponent();
            canEdit = false;
            this.BindingContext = this;
            canEdit = false;
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
            grid.BindingContext = mhistory;
            scrollView.BindingContext = this;

            //Create a row definition for each field
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

            editButton.IsEnabled = true;

            scrollView.Content = grid;
        }

        // Places Lables and Entrys into grid
        private void addGridRow(ref Grid grid, string labelText, string binding, int rownumber) {
            Color bgColor;

            if ( rownumber % 2 == 0 ) {
                bgColor = Color.Gray;
            }else {
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
            entry.SetBinding( Entry.TextProperty, binding );
            entry.SetBinding( Entry.IsEnabledProperty, "canEdit" );
            Grid.SetRow( entry, rownumber );
            Grid.SetColumn( entry, 1 );
            grid.Children.Add( entry );
        }

        private void OnSaveButtonClicked( object sender, EventArgs e ) {          
            app.dbConnection.Update( mhistory );
            canEdit = false;  
        }

        private void OnEditButtonClicked( object sender, EventArgs e ) {
            canEdit = true;
        }

        async private void OnbackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }
    }
}
