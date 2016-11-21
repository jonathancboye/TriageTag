using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace TriageTagApplication.Droid
{
    [Activity( Label = "TriageTagApplication", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int NFC_READ = 0;
        public static int NFC_WRITE = 1;
        private Action<int, Result, Intent> _resultCallback;

        protected override void OnCreate( Bundle bundle ) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate( bundle );

            global::Xamarin.Forms.Forms.Init( this, bundle );
            LoadApplication( new App() );
        }

       public void startActivity(Type typeofActivity, Action<int, Result, Intent> resultCallback, int resultCode ) {
            _resultCallback = resultCallback;
            StartActivityForResult( typeofActivity, resultCode );
        }

       protected override void OnActivityResult( int requestCode, Result resultCode, Intent data ) {
            base.OnActivityResult( requestCode, resultCode, data );
            _resultCallback( requestCode, resultCode, data );
            _resultCallback = null;
        }
    }
}

