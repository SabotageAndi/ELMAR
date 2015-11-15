using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;

namespace elmar.droid.Voice
{
    class TTSChecker
    {
        private readonly VoiceOutput _voiceOutput;
        public const int CheckCode = 1;

        private  Activity _activity;

        public TTSChecker(VoiceOutput voiceOutput)
        {
            _voiceOutput = voiceOutput;
        }

        public void checkTTS()
        {
            var intent = new Intent(TextToSpeech.Engine.ActionCheckTtsData);

            if (_activity != null)
                _activity.StartActivityForResult(intent, CheckCode);
        }

        public void SetActivity(Activity activity)
        {
            _activity = activity;
        }

        public void onResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == CheckCode)
            {
                if ((int)resultCode == (int)CheckVoiceData.Pass)
                {
                    _voiceOutput.Enable();                    
                }
                else
                {
                    var intent = new Intent(TextToSpeech.Engine.ActionInstallTtsData);
                    _activity.StartActivity(intent);
                }
            }
        }
    }
}