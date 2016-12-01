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
    [Activity( Label = "WriteActivity" )]
    public class WriteActivity : Activity
    {
        NfcAdapter nfcAdapter;
        PendingIntent pendingintent;
        IntentFilter[] intentFilters;
        byte[] messageToWrite;
        bool writingToTage;

        //string appMime = "application/TriageTagApplication";

        protected override void OnCreate( Bundle savedInstanceState ) {
            base.OnCreate( savedInstanceState );

            SetContentView( Resource.Layout.WriterActivity2 );
            Button writeButton = FindViewById<Button>(Resource.Id.writeButton);

            initializeScrollView();

            // Get NFC Adapter
            nfcAdapter = NfcAdapter.GetDefaultAdapter( this );

            // NFC Adapter not found
            if ( nfcAdapter == null || nfcAdapter.IsEnabled == false ) {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle( "NFC Adapter not found" );
                alert.SetMessage( "Your device does not support NFC or is disabled. Please Enable the NFC Adapter and try again." );
                alert.SetPositiveButton( "OK", ( s, e ) => {
                    Finish();
                } );
                Dialog dialog = alert.Create();
                dialog.Show();
            }

            var intentFilter = new IntentFilter(NfcAdapter.ActionTagDiscovered);
            var intent = new Intent( this, GetType() ).AddFlags( ActivityFlags.SingleTop );
            intentFilters = new[] { intentFilter };
            pendingintent = PendingIntent.GetActivity( this, 0, intent, 0 );

            writeButton.Click += OnWriteButtonClicked;
        }

        private void initializeScrollView() {
            TextView employeeId = FindViewById<TextView>(Resource.Id.employeeId);
            LinearLayout linLayout = FindViewById<LinearLayout>(Resource.Id.linearLayout);
            List<DecryptedEmployee> employees = Database.getAllEmployees();

            for(int i=0; i < employees.Count; i++) {
                DecryptedEmployee employee = employees[i];
                TextView tview = new TextView(this) {
                    Text = employee.lastname + ", " + employee.firstname,
                    Clickable = true,
                    Gravity = GravityFlags.Center,
                    TextSize = 30,
                };

                if(i%2 == 0 ) {
                    tview.SetBackgroundColor( Android.Graphics.Color.Brown);
                }else {
                    tview.SetBackgroundColor( Android.Graphics.Color.Purple);
                }

                tview.SetPadding( 0, 60, 0, 60 );
                tview.Click += delegate {
                    employeeId.Text = employee.employeeId;
                };

                linLayout.AddView(tview);
            }

        }

        private void OnWriteButtonClicked( object sender, EventArgs eventArgs ) {
            TextView employeeId = FindViewById<TextView>(Resource.Id.employeeId);

            if ( employeeId.Text == string.Empty ) {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle( "Enter Employee ID" );
                alert.SetMessage( "You must enter an Employee ID to write to tag" );
                alert.SetPositiveButton( "OK", ( s, e ) => {
                    AlertDialog d = s as AlertDialog;
                    d.Cancel();
                } );
                Dialog dialog = alert.Create();
                dialog.Show();
            } else {
                if ( !writingToTage ) {
                    messageToWrite = Crypto.EncryptAes(employeeId.Text, App.pkey, App.salt);
                    nfcAdapter.EnableForegroundDispatch( this, pendingintent, intentFilters, null );
                    writingToTage = true;
                }
            }
        }

        protected override void OnPause() {
            base.OnPause();
            nfcAdapter.DisableForegroundDispatch( this );
        }

        protected override void OnResume() {
            base.OnResume();
            if ( writingToTage == true ) {
                nfcAdapter.EnableForegroundDispatch( this, pendingintent, intentFilters, null );
            } else {
                nfcAdapter.DisableForegroundDispatch( this );
            }
        }

        protected override void OnNewIntent( Intent intent ) {
            Tag tag = intent.GetParcelableExtra( NfcAdapter.ExtraTag ) as Tag;
            if ( messageToWrite == null ) {
                System.Diagnostics.Debug.WriteLine( "Empty String" );
                return;
            } 
            NdefRecord ndfRecord = NdefRecord.CreateMime( "Application/TriageTag", messageToWrite );
            NdefMessage ndfMessage = new NdefMessage( ndfRecord );

            var ndef = Ndef.Get(tag);
            if ( ndef != null ) {
                try {
                    ndef.Connect();
                    ndef.WriteNdefMessage( ndfMessage );
                    ndef.Close();
                }catch (Exception exception ) {
                    System.Diagnostics.Debug.WriteLine( exception.ToString() );
                    writingToTage = false;
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle( "ERROR" );
                    alert.SetMessage( "Failed to write tag.Try again" );
                    alert.SetPositiveButton( "OK", ( s, e ) => {
                        AlertDialog d = s as AlertDialog;
                        d.Cancel();
                    } );
                    Dialog dialog = alert.Create();
                    dialog.Show();
                    return;
                }
                ndef.Close();
                SetResult( Result.Ok );
            } else {
                SetResult( Result.Canceled );
            }
            Finish();
        }
    }
}