﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TriageTagApplication
{
    public partial class ActivitiesPage : ContentPage
    {
        App application = Application.Current as App;
        public bool syncComplete = false;


        public ActivitiesPage() {
            InitializeComponent();

            // Hide certain buttons depending on platfrom
            if(Device.OS == TargetPlatform.Windows ) {

            }else if(Device.OS == TargetPlatform.Android ) {
                editMedicalDataButton.IsVisible = false;
                addUserButton.IsVisible = false;
            }
        }

        async private void OnLogoutButtonClicked( object sender, EventArgs e ) {
            await Navigation.PopAsync();
        }

        async private void OnaddUserButtonClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new addUserPage());
        }

        async private void OnScanTagButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new ScanPage() );
        }

        async private void OnEditMedicalDataButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new EditMedicalDataPage() );
        }

        async private void OnSynchronizeButtonClicked( object sender, EventArgs e ) {
            await Navigation.PushAsync( new SynchronizingPage() );
        }
    }
}
