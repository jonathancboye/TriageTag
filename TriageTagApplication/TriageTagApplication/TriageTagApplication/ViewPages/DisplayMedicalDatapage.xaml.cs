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
        App app = Application.Current as App;
        Grid grid;

        public DisplayMedicalDatapage() {
            InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            // Check for medical history
            MedicalHistory mhistory;
            List<MedicalHistory> mhistorys = app.dbConnection.Query<MedicalHistory>( "SELECT * FROM MedicalHistory WHERE employeeId=?", app.UID );
            if ( mhistorys.Count > 0 ) {
                mhistory = mhistorys[0];
            } else {
                // No medical history found
                System.Diagnostics.Debug.WriteLine( "No medical history" );
                return;
            }

            grid = new Grid();

            //Create a row definition for each field
            for ( int i = 0; i < 6; i++ ) {
                grid.RowDefinitions.Add( new RowDefinition() { Height = new GridLength( 1, GridUnitType.Star ) } );
            }

            // column definition for name of medical history
            grid.ColumnDefinitions.Add( new ColumnDefinition() );
            // column definition for value of medical history 
            grid.ColumnDefinitions.Add( new ColumnDefinition() );


            // Key Labels to add to grid

            List<Label> k_labels = new List<Label> {
                new Label {
                    Text = "Employee ID"
                },
                new Label {
                    Text = "Allergies"
                },
                new Label {
                    Text = "Blood Type"
                },
                new Label {
                    Text = "Religion"
                },
                new Label {
                    Text = "Medications"
                },
                new Label {
                    Text = "Primary Doctor"
                }
            };

            //Value Labels to add to grid
            List<Label> v_labels = new List<Label> {

                new Label {
                    Text = mhistory.employeeId.ToString()
                },
                new Label {
                    Text = mhistory.allergies
                },
                new Label {
                    Text = mhistory.bloodType
                },
                new Label {
                    Text = mhistory.highBloodPressure.ToString()
                },
                new Label {
                    Text = mhistory.medications
                },
                new Label {
                    Text = mhistory.primaryDoctor
                }
            };

            // Set Key Labes in grid
            for ( int i = 0; i < k_labels.Count; i++ ) {
                Grid.SetRow( k_labels[i], i );
                Grid.SetColumn( k_labels[i], 0 );
                grid.Children.Add( k_labels[i] );
            }

            // Set Value Labels in grid
            for ( int i = 0; i < v_labels.Count; i++ ) {
                Grid.SetRow( v_labels[i], i );
                Grid.SetColumn( k_labels[i], 1 );
                grid.Children.Add( v_labels[i] );
            }

            scrollView.Content = grid;
        }

        async private void OnBackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();    
        }
    }
}
