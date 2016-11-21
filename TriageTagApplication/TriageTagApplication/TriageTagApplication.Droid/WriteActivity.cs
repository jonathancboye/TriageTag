using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Nfc;
using Android.Nfc.Tech;
using Xamarin.Forms;

namespace TriageTagApplication.Droid
{
    [Activity( Label = "WriteActivity" )]
    public class WriteActivity : Activity
    {
        NfcAdapter nfcAdapter;
        PendingIntent pendingintent;
        IntentFilter[] intentFilters;
        //string appMime = "application/TriageTagApplication";
        string messageToWrite;

        protected override void OnCreate( Bundle savedInstanceState ) {
            base.OnCreate( savedInstanceState );
            nfcAdapter = NfcAdapter.GetDefaultAdapter( this );
            if ( nfcAdapter == null ) {
                System.Diagnostics.Debug.WriteLine( "Could not find nfc adapter" );
                Finish();
            }
            messageToWrite = Intent.GetStringExtra( "message" );
            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var intentFilter = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            intentFilters = new[] { intentFilter };
            pendingintent = PendingIntent.GetActivity( this, 0, intent, PendingIntentFlags.OneShot );      
        }

        protected override void OnPause() {
            base.OnPause();
            nfcAdapter.DisableForegroundDispatch( this );
        }

        protected override void OnResume() {
            base.OnResume();
            nfcAdapter.EnableForegroundDispatch( this, pendingintent, intentFilters, null );
        }

        protected override void OnNewIntent( Intent intent ) {
            Tag tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
            NdefRecord ndfRecord = NdefRecord.CreateMime(System.Net.Mime.MediaTypeNames.Text.Plain,
               Encoding.ASCII.GetBytes(messageToWrite));
            NdefMessage ndfMessage = new NdefMessage(ndfRecord);

            var ndef = Ndef.Get(tag);
            if(ndef != null ) {
                ndef.Connect();
                ndef.WriteNdefMessage( ndfMessage );
                ndef.Close();

                SetResult( Result.Ok );
            }else {
                SetResult( Result.Canceled );
            }
            Finish();
        }
    }
}