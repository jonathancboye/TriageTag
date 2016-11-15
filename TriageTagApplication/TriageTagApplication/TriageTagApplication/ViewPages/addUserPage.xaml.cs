using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication.ViewPages
{
    public partial class addUserPage : ContentPage
    {
        public addUserPage()
        {
            //InitializeComponent();
        }

        /*
        Check if the user has entered text into the fields 
        if text is present: enable login button
        else button is disabled.
            */
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = (Entry)sender;

            
        }


    }
}
