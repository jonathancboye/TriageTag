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
    [Activity( Label = "ReadActivity" )]
    public class ReadActivity : Activity
    {
        NfcAdapter nfcAdapter;
        PendingIntent pendingintent;
        IntentFilter[] intentFilters;

        protected override void OnCreate( Bundle savedInstanceState ) {
            base.OnCreate( savedInstanceState );
            nfcAdapter = NfcAdapter.GetDefaultAdapter( this );
            if ( nfcAdapter == null ) {
                System.Diagnostics.Debug.WriteLine( "Could not find nfc adapter" );
                Finish();
            }
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
            var ndef = Ndef.Get(tag);
            if ( ndef != null ) {
                ndef.Connect();

                NdefMessage ndfMessage = ndef.NdefMessage;
                NdefRecord[] ndfRecords = ndfMessage.GetRecords();
                string payload = new string( Encoding.ASCII.GetChars( ndfRecords[0].GetPayload() ) );
                if ( ndfRecords != null ) {
                    Intent returnIntent = new Intent();
                    returnIntent.PutExtra( "message", payload );
                    SetResult( Result.Ok, returnIntent );
                } else {
                    System.Diagnostics.Debug.WriteLine( "No NdfRecords" );
                    SetResult( Result.Canceled );
                }
                ndef.Close();
            }

            Finish();
        }
    }
}