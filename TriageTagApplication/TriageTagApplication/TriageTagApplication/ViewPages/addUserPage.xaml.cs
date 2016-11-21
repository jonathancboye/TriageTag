using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class addUserPage : ContentPage
    {

        App app = Application.Current as App;

        private byte[] emId;
        

        public addUserPage()
        {
            InitializeComponent();

        }

        async private void goBack()
        {

            await Navigation.PopAsync();
        }

        private void OngenEmClicked(object sender, EventArgs E)
        {

            if (formComplete())
            {
                string tempEmID = fnmField.Text + lnField.Text + new Random().Next(1, 1000).ToString();
                emError.IsVisible = false;
                emId = Crypto.EncryptAes(tempEmID, App.pkey, app.salt);
                emField.Text = tempEmID;
                writeEm.IsVisible = true;

            }
            else { emError.IsVisible = true; writeEm.IsVisible = false; }

        }
        //write the emID to the tag as employeeID
        private void OnwriteEmClicked(object sender, EventArgs E)
        {

        }



        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            
            if (formComplete())
            {

                if (checkUserName() && checkUserLvl() )
                {

                    app.dbConnection.Insert(new Users
                    {
                        employeeId = emId,
                        username = Crypto.EncryptAes(userField.Text, App.pkey, app.salt),
                        password = Crypto.EncryptAes(passField.Text, App.pkey, app.salt),
                        userLvl = Crypto.EncryptAes(ulvlField.Text, App.pkey, app.salt)
                    });



                    app.dbConnection.Insert(new Employee
                    {
                        employeeId = emId,
                        address = Crypto.EncryptAes(addressField.Text, App.pkey, app.salt),
                        phonenumber = Crypto.EncryptAes(phoneField.Text, App.pkey, app.salt),
                        emergencyContact = Crypto.EncryptAes(emergField.Text, App.pkey, app.salt),
                        firstname = Crypto.EncryptAes(fnmField.Text, App.pkey, app.salt),
                        lastname = Crypto.EncryptAes(lnField.Text, App.pkey, app.salt)
                    });

                    errorReset();
                    clearFields();
                    DisplayAlert("User added", "User" + fnmField.Text + " " + lnField.Text + " was succesfully added.", "OK");
                    goBack();
                }

            }

        }

        private Boolean checkUserLvl()
        {
            Boolean valid = true;
            int lvl;
            Boolean parsed = Int32.TryParse(ulvlField.Text, out lvl);

            if (!parsed)
            {
                lvlError.IsVisible = true;
                lvlError.Text = "User lvl must be integer.";
                valid = false;
            }
            else
            {
                if(lvl != 1 && lvl != 2){
                    lvlError.IsVisible = true;
                    lvlError.Text = "User lvl must be either 1 or 2";
                    valid = false;
                }
            }

            return valid;

        }
        /*Ensure username isn't already in use. */
        private Boolean checkUserName()
        {
            Boolean valid = true;
            String uName = "";

            List<Users> users = app.dbConnection.Query<Users>("SELECT username FROM Users");

            foreach(Users name in users)
            {

                try
                {
                    uName = Crypto.DecryptAes(name.username, App.pkey, app.salt);

                    if(uName == userField.Text) { valid = false;userError.IsVisible = true; userError.Text = "Username already in use.";  break; }
                }catch { continue; }

            }

            return valid;
        }

        private Boolean formComplete()
        {
            Boolean completed = true;

            if (fnmField.Text == null || fnmField.Text == "")
            {
                completed = false;
                firstError.IsVisible = true;
            }
            else firstError.IsVisible = false;

            if (lnField.Text == null || lnField.Text == "")
            {
                completed = false;
                lastError.IsVisible = true;
            }

            else lastError.IsVisible = false;

            if (addressField.Text == null || addressField.Text == "")
            {
                completed = false;
                addressError.IsVisible = true;
            }
            else addressError.IsVisible = false;

            if (phoneField.Text == null || phoneField.Text == "")
            {
                completed = false;
                phoneError.IsVisible = true;
            }
            else phoneError.IsVisible = false;

            if (emergField.Text == null || emergField.Text == "")
            {
                completed = false;
                emergError.IsVisible = true;
            }
            else emergError.IsVisible = false;

            if (userField.Text == null || userField.Text == "")
            {
                completed = false;
                userError.IsVisible = true;
            }
            else userError.IsVisible = false;

            if (passField.Text == null || passField.Text == "")
            {
                completed = false;
                passError.IsVisible = true;
            }
            else passError.IsVisible = false;

            if (ulvlField.Text == null || ulvlField.Text == "")
            {
                completed = false;
                lvlError.IsVisible = true;
            }
            else lvlError.IsVisible = false;

            return completed;
        }

        private void errorReset()
        {
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

        private void clearFields()
        {
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
