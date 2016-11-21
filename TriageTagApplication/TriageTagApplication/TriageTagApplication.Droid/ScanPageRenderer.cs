using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TriageTagApplication.Droid;
using TriageTagApplication;

[assembly: ExportRenderer( typeof( ScanPage ), typeof( ScanPageRenderer ) )]
namespace TriageTagApplication.Droid
{
    class ScanPageRenderer : PageRenderer
    {
        protected override void OnElementChanged( ElementChangedEventArgs<Page> e ) {
            base.OnElementChanged( e );

            var oldFirstPage = e.OldElement as ScanPage;
            if ( oldFirstPage != null ) {
                oldFirstPage.readButton.Clicked -= OnReadButtonClicked;
                oldFirstPage.writeButton.Clicked -= OnWriteButtonClicked;
            }

            var newScanPage = e.NewElement as ScanPage;
            if ( newScanPage != null ) {
                newScanPage.readButton.Clicked += OnReadButtonClicked;
                newScanPage.writeButton.Clicked += OnWriteButtonClicked;
            }
        }

        private void OnReadButtonClicked( object sender, EventArgs e ) {
            var mainActivity = this.Context as MainActivity;
            mainActivity.startActivity( typeof(ReadActivity), OnActivityResult, MainActivity.NFC_READ );
        }

        private void OnWriteButtonClicked( object sender, EventArgs e ) {
            var mainActivity = this.Context as MainActivity;
            var scanPage = this.Element as ScanPage;
            mainActivity.startActivity( typeof( WriteActivity ), OnActivityResult, MainActivity.NFC_WRITE );
        }

        private void OnActivityResult( int requestCode, Result resultCode, Intent data ) {
            var scanPage = this.Element as ScanPage;
            string message;
            if ( resultCode == Result.Ok ) {
                if ( requestCode == MainActivity.NFC_READ ) {
                    message = data.GetStringExtra( "message" );
                    scanPage.OnReadButtonClicked( message );
                } else if ( requestCode == MainActivity.NFC_WRITE ) {
                    message = "wrote mesasge";
                    scanPage.OnWriteButtonClicked( message );
                }
            }
        }
    }
}