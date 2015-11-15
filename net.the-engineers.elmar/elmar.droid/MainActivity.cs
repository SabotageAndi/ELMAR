using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using elmar.droid.Voice;
using TinyIoC;

namespace elmar.droid
{
    [Activity(Label = "elmar.droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ImageButton _startVoiceButton;
        private ImageButton _preferenceButton;


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

            _startVoiceButton = FindViewById<ImageButton>(Resource.Id.startVoice);
            _preferenceButton = FindViewById<ImageButton>(Resource.Id.preferences);

            _startVoiceButton.Click += StartVoiceButtonOnClick;
        }

        private void StartVoiceButtonOnClick(object sender, EventArgs eventArgs)
        {
            var voiceRecognizer = new VoiceRecognizer(this);
            voiceRecognizer.StartVoiceRecognition();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var ttsChecker = TinyIoCContainer.Current.Resolve<TTSChecker>();
            ttsChecker.onResult(requestCode, resultCode, data);
        }
    }
}

