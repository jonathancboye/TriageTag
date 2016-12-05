using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class DeleteUserPage : ContentPage
    {

        List<String> usersNames;
        String currentFirst = "";
        String currentLast = "";
        byte[] emID = null;

        Picker picker = new Picker
        {
            Title = "Users",

            WidthRequest = 200
        };
        public DeleteUserPage()
        {
            InitializeComponent();

            

            setUserNames();

            foreach(String nm in usersNames)
            {
                picker.Items.Add(nm);

            }

            picker.SelectedIndexChanged += (sender, args) =>
            {
                 
                string name = picker.Items[picker.SelectedIndex];
                string[] splitName = name.Split(' ');
                currentFirst = splitName[0];
                currentLast = splitName[1];

                if(currentFirst != "" && currentLast != "")
                {
                    emID = Database.getEmployeeIdFromName(Database.encrypt(currentFirst), Database.encrypt(currentLast));
                }
                
            };

            Button button = new Button
            {
                Text = "Delete User",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked;

            // Build the page.
            this.Content = new StackLayout
            
            {
                
                Children =
                {
                    
                    picker,
                    button
                   
                    
                }
            };
        }

        private void setUserNames()
        {
            usersNames = Database.getAllusers();

        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            if(emID != null)
            {
                Database.deleteUser(emID);
                picker.Items.Remove(currentFirst + " " + currentLast);
            }
           
        }

        
    }
}
