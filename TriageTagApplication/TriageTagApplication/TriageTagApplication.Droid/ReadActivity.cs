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

namespace TriageTagApplication.Droid
{
    [Activity( Label = "ReadActivity" )]
    public class ReadActivity : Activity
    {
        NfcAdapter nfcAdapter;
        PendingIntent pendingintent;
        IntentFilter[] intentFilters;
        bool readingFromTag;

        protected override void OnCreate( Bundle savedInstanceState ) {
            base.OnCreate( savedInstanceState );

            SetContentView( Resource.Layout.ReaderActivity );
            Button readButton = FindViewById<Button>(Resource.Id.readButton);


            // Get NFC Adapter
            nfcAdapter = NfcAdapter.GetDefaultAdapter( this );

            // NFC Adapter not found
            if ( nfcAdapter == null || nfcAdapter.IsEnabled == false ) {
                var statusText = FindViewById<TextView>( Resource.Id.statusText );
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle( "NFC Adapter not found" );
                alert.SetMessage( "Your device does not support NFC or is disabled. Please Enable the NFC Adapter and try again." );
                alert.SetPositiveButton( "OK", ( s, e ) => {
                    Finish();
                } );
                Dialog dialog = alert.Create();
                dialog.Show();
            }

            // Launch pendingintent when NFC Tag is discovered
            // When Tag is discovered OnNewIntent is called
            var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            var intentFilter = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            intentFilters = new[] { intentFilter };
            pendingintent = PendingIntent.GetActivity( this, 0, intent, 0);

            readButton.Click += OnReadButtonClicked;
        }

        private void OnReadButtonClicked( object sender, EventArgs eventArgs ) {
            nfcAdapter.EnableForegroundDispatch( this, pendingintent, intentFilters, null );
            readingFromTag = true;
        }

        protected override void OnPause() {
            base.OnPause();
            nfcAdapter.DisableForegroundDispatch( this );
        }

        protected override void OnResume() {
            base.OnResume();
            if ( readingFromTag == true ) {
                nfcAdapter.EnableForegroundDispatch( this, pendingintent, intentFilters, null );
            } else {
                nfcAdapter.DisableForegroundDispatch( this );
            }
        }

        // Open a connection to the NFC tag and read when NFC Tag is discovered
        protected override void OnNewIntent( Intent intent ) {
            Tag tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
            var ndef = Ndef.Get(tag);
            if ( ndef != null ) {
                try {
                    ndef.Connect();

                    NdefMessage ndfMessage = ndef.NdefMessage;
                    NdefRecord[] ndfRecords = ndfMessage.GetRecords();
                    string payload = Crypto.DecryptAes(ndfRecords[0].GetPayload(), App.pkey, App.salt);
                    System.Diagnostics.Debug.WriteLine( "Message Read: " + payload );
                    if ( ndfRecords != null ) {
                        Intent returnIntent = new Intent();
                        returnIntent.PutExtra( "message", payload );
                        SetResult( Result.Ok, returnIntent );
                    } else {
                        System.Diagnostics.Debug.WriteLine( "No NdfRecords" );
                        SetResult( Result.Canceled );
                    }
                } catch ( Exception exception ) {
                    System.Diagnostics.Debug.WriteLine( exception.ToString() );

                    readingFromTag = false;

                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle( "ERROR" );
                    alert.SetMessage( "Failed to read tag. Try again" );
                    alert.SetPositiveButton( "OK", ( s, e ) => {
                        AlertDialog d = s as AlertDialog;
                        d.Cancel();
                    } );
                    Dialog dialog = alert.Create();
                    dialog.Show();
                    return;
                }
                ndef.Close();
                Finish();
            }
        }
    }
}