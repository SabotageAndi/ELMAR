using System;
using Android.App;
using Android.Content;
using Android.Media.Session;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MediaController = Android.Media.Session.MediaController;

namespace elmar.droid
{
    [BroadcastReceiver()]
    [IntentFilter(new [] {Constants.ELMAR_MEDIABUTTON_PRESSED})]

    public class MediaButtonBroadCastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var intentAction = intent.Action;
            Toast.MakeText(context, "Key pressed", ToastLength.Long).Show();
        }
    }


  

    [Activity(Label = "elmar.droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private MediaSession mediaSession;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            var serviceIntent = new Intent(this, typeof(MediaButtonService));
            StartService(serviceIntent);
        }
    }
}

