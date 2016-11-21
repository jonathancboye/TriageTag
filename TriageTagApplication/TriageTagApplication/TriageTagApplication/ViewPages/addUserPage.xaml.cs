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


        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            
            if (formComplete())
            {

                if (checkUserName() && checkUserLvl() )
                {

                    
                    emId = Crypto.EncryptAes(new Random().Next(1, 1000).ToString(), passField.Text, app.salt);
                    app.dbConnection.Insert(new Users
                    {
                        employeeId = emId,
                        username = Crypto.EncryptAes(userField.Text, passField.Text, app.salt),
                        password = Crypto.EncryptAes(passField.Text, passField.Text, app.salt),
                        userLvl = Crypto.EncryptAes(ulvlField.Text, passField.Text, app.salt)
                    });



                    app.dbConnection.Insert(new Employee
                    {
                        employeeId = emId,
                        address = Crypto.EncryptAes(addressField.Text, passField.Text, app.salt),
                        phonenumber = Crypto.EncryptAes(phoneField.Text, passField.Text, app.salt),
                        emergencyContact = Crypto.EncryptAes(emergField.Text, passField.Text, app.salt),
                        firstname = Crypto.EncryptAes(fnmField.Text, passField.Text, app.salt),
                        lastname = Crypto.EncryptAes(lnField.Text, passField.Text, app.salt)
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

        private Boolean checkUserName()
        {
            Boolean valid = true;
            String uName = "";

            List<Users> users = app.dbConnection.Query<Users>("SELECT username FROM Users");

            foreach(Users name in users)
            {
                uName = Crypto.DecryptAes(name.username,)

            }
            

            if(users.Count > 0)
            {
                valid = false;
                userError.IsVisible = true;
                userError.Text = "User name unavailable";
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

        }

    }
}
