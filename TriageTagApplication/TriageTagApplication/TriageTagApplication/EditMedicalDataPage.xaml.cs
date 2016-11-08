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

        public EditMedicalDataPage() {
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
            Type type = typeof(MedicalHistory);
            List<FieldInfo> fields = type.GetRuntimeFields() as List<FieldInfo>;

            //Create a row definition for each field
            foreach ( FieldInfo field in fields ) {
                grid.RowDefinitions.Add( new RowDefinition() );
            }

            // column definition for name of medical history
            grid.ColumnDefinitions.Add( new ColumnDefinition() );
            // column definition for value of medical history 
            grid.ColumnDefinitions.Add( new ColumnDefinition() );


            // Labels to add to grid
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
        }

        private void OnSaveButtonClicked( object sender, EventArgs e ) {
            //TODO Implement OnSaveButtonClicked
        }

        private void OnCancelButtonClicked( object sender, EventArgs e ) {
            app.MainPage = app.activitiesPage;
        }
    }
}
