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
        Grid grid;
        List<Entry> entrys;
        List<Label> labels;

        public EditMedicalDataPage() {
            InitializeComponent();
            makeGrid();
        }

        private void makeGrid() {
            int numberOfLables = 6; // Number of columns in the Medical History Table

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
            for ( int i = 0; i < numberOfLables; i++ ) {
                grid.RowDefinitions.Add( new RowDefinition() { Height = new GridLength( 0.5, GridUnitType.Star ) } );
            }

            // column definition for name of medical history
            grid.ColumnDefinitions.Add( new ColumnDefinition() { Width = new GridLength( 1, GridUnitType.Star ) } );
            // column definition for value of medical history 
            grid.ColumnDefinitions.Add( new ColumnDefinition() { Width = new GridLength( 3, GridUnitType.Star ) } );

            // Labels to add to grid      
            labels = new List<Label> {
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

            // Entrys to add to grid
            entrys = new List<Entry> {

                new Entry {
                    Text = mhistory.employeeId.ToString(),
                },
                new Entry {
                    Text = mhistory.allergies,
                },
                new Entry {
                    Text = mhistory.bloodType,
                },
                new Entry {
                    Text = mhistory.highBloodPressure.ToString(),
                },
                new Entry {
                    Text = mhistory.medications,
                },
                new Entry {
                    Text = mhistory.primaryDoctor,
                }
            };

            // put labes in grid
            for ( int i = 0; i < labels.Count; i++ ) {
                Grid.SetRow( labels[i], i );
                Grid.SetColumn( labels[i], 0 );

                labels[i].HorizontalTextAlignment = TextAlignment.Center;
                if ( i % 2 == 0 ) {
                    labels[i].BackgroundColor = Color.Aqua;
                }

                grid.Children.Add( labels[i] );
            }

            // put entrys in grid
            for ( int i = 0; i < entrys.Count; i++ ) {
                // format entrys in grid
                Grid.SetRow( entrys[i], i );
                Grid.SetColumn( entrys[i], 1 );

                entrys[i].HorizontalTextAlignment = TextAlignment.Center;
                entrys[i].IsEnabled = false;
                
                grid.Children.Add( entrys[i] );
            }

            editButton.IsEnabled = true;

            scrollView.Content = grid;
        }

        private void isEditable(bool editable) {
            for(int i = 0; i < entrys.Count; i++ ) {
                entrys[i].IsEnabled = editable;
            }
        }

        private void OnSaveButtonClicked( object sender, EventArgs e ) {
            // TODO: Save changes to database

            isEditable( false );
        }

        private void OnEditButtonClicked( object sender, EventArgs e ) {
            isEditable( true );
        }

        async private void OnbackButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();

        }
    }
}
