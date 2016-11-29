using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class AddUserPage : ContentPage
    {
        private string emId;

        public AddUserPage() {
            InitializeComponent();
        }

        async private void goBack() {

            await Navigation.PopAsync();
        }

        private void OngenEmClicked( object sender, EventArgs E ) {

            if ( formComplete() ) {
                emId = fnmField.Text + lnField.Text + new Random().Next( 1, 1000 ).ToString();
                emError.IsVisible = false;
                emField.Text = emId;
                writeEm.IsVisible = true;

            } else { emError.IsVisible = true; writeEm.IsVisible = false; }
        }

        async private void OnSaveButtonClicked( object sender, EventArgs e ) {

            if ( formComplete() ) {

                if ( checkUserName() && checkUserLvl() ) {

                    App.dbConnection.Insert( 
                        Database.encryptUser(
                            new DecryptedUser {
                                employeeId = emId,
                                username = userField.Text,
                                password = passField.Text,
                                userLvl = ulvlField.Text
                            } ) );

                    App.dbConnection.Insert( 
                        Database.encryptEmployee(
                            new DecryptedEmployee {
                                employeeId = emId,
                                address = addressField.Text,
                                phonenumber = phoneField.Text,
                                emergencyContact = emergField.Text,
                                firstname = fnmField.Text,
                                lastname = lnField.Text,
                            } ) );

                    App.dbConnection.Insert(
                        Database.encryptMedicalHistory(
                            new DecryptedMedicalHistory {
                                employeeId = emId,
                                allergies = "",
                                bloodType = "",
                                religion = "",
                                highBloodPressure = "",
                                medications = "",
                                primaryDoctor = "",
                            } ) );

                    errorReset();
                    clearFields();

                    // Update the ftp server Database
                    await DependencyService.Get<ISQLite>().copyFileToFtpServer(App.DatabaseFilename);

                    await DisplayAlert( "User added", "User" + fnmField.Text + " " + lnField.Text + " was succesfully added.", "OK" );
                    goBack();
                }
            }
        }

        private bool checkUserLvl() {
            bool valid = true;
            int lvl;
            bool parsed = Int32.TryParse(ulvlField.Text, out lvl);

            if ( !parsed ) {
                lvlError.IsVisible = true;
                lvlError.Text = "User lvl must be integer.";
                valid = false;
            } else {
                if ( lvl != 1 && lvl != 2 ) {
                    lvlError.IsVisible = true;
                    lvlError.Text = "User lvl must be either 1 or 2";
                    valid = false;
                }
            }

            return valid;
        }

        /*Ensure username isn't already in use. */
        private bool checkUserName() {
            if ( Database.isUsernameTaken( userField.Text ) ) {
                userError.IsVisible = true;
                userError.Text = "Username already in use.";
                return false;
            }
            return true;
        }

        private bool formComplete() {
            bool completed = true;

            if ( fnmField.Text == null || fnmField.Text == "" ) {
                completed = false;
                firstError.IsVisible = true;
            } else firstError.IsVisible = false;

            if ( lnField.Text == null || lnField.Text == "" ) {
                completed = false;
                lastError.IsVisible = true;
            } else lastError.IsVisible = false;

            if ( addressField.Text == null || addressField.Text == "" ) {
                completed = false;
                addressError.IsVisible = true;
            } else addressError.IsVisible = false;

            if ( phoneField.Text == null || phoneField.Text == "" ) {
                completed = false;
                phoneError.IsVisible = true;
            } else phoneError.IsVisible = false;

            if ( emergField.Text == null || emergField.Text == "" ) {
                completed = false;
                emergError.IsVisible = true;
            } else emergError.IsVisible = false;

            if ( userField.Text == null || userField.Text == "" ) {
                completed = false;
                userError.IsVisible = true;
            } else userError.IsVisible = false;

            if ( passField.Text == null || passField.Text == "" ) {
                completed = false;
                passError.IsVisible = true;
            } else passError.IsVisible = false;

            if ( ulvlField.Text == null || ulvlField.Text == "" ) {
                completed = false;
                lvlError.IsVisible = true;
            } else lvlError.IsVisible = false;

            return completed;
        }

        private void errorReset() {
            firstError.IsVisible = false;
            lastError.IsVisible = false;
            addressError.IsVisible = false;
            phoneError.IsVisible = false;
            emergError.IsVisible = false;
            userError.IsVisible = false;
            userError.Text = "Field Empty";
            passError.IsVisible = false;
            lvlError.IsVisible = false;
            lvlError.Text = "Field Empty";
            emError.IsVisible = false;
        }

        private void clearFields() {
            fnmField.Text = "";
            lnField.Text = "";
            addressField.Text = "";
            phoneField.Text = "";
            emergField.Text = "";
            userField.Text = "";
            passField.Text = "";
            ulvlField.Text = "";
            writeEm.IsVisible = false;
            emField.Text = "";
        }

    }
}
