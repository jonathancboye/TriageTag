using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class LoginPage : ContentPage
    {
        App application = Application.Current as App;

        public LoginPage() {
            Padding = new Thickness( 5, 20, 5, 20 );
            InitializeComponent();
        }
    }
}
