using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using elmar.droid.Voice;
using TinyIoC;

namespace elmar.droid
{
    [BroadcastReceiver()]
    [IntentFilter(new [] {Constants.ELMAR_MEDIABUTTON_PRESSED})]

    public class MediaButtonBroadCastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Key pressed", ToastLength.Long).Show();
        }
    }


  

    [Activity(Label = "elmar.droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();

            var serviceIntent = new Intent(this, typeof(MediaButtonService));
            StartService(serviceIntent);


            var ttsChecker = TinyIoCContainer.Current.Resolve<TTSChecker>();
            ttsChecker.SetActivity(this);
            ttsChecker.checkTTS();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var ttsChecker = TinyIoCContainer.Current.Resolve<TTSChecker>();
            ttsChecker.onResult(requestCode, resultCode, data);
        }
    }
}

