using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace elmar.droid
{
    class VoiceOutput : Java.Lang.Object, TextToSpeech.IOnInitListener, IDisposable
    {
        private bool _init = false;
        private readonly TextToSpeech _tts;
        private bool _enabled = false;

        public VoiceOutput()
        {
            _tts = new TextToSpeech(Application.Context, this);
        }


        public void OnInit(OperationResult status)
        {
            if (status == OperationResult.Success)
            {
                _tts.SetLanguage(Locale.Default);
                _init = true;
            }
            else
            {
                _init = false;
            }
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Speek(string text)
        {
            if (_enabled && _init)
            {
                var bundle = new Bundle();
                bundle.PutString(TextToSpeech.Engine.KeyParamStream, ((int)Stream.Notification).ToString());

                _tts.Speak(text, QueueMode.Add, bundle, "");
            }
        }

        protected override void Dispose(bool disposing)
        {
            _tts.Shutdown();
            base.Dispose(disposing);
        }
    }
}