using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media.Session;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MediaController = Android.Media.Session.MediaController;

namespace elmar.droid
{
    [Service]
    class MediaButtonService : Service
    {
        private MediaSession _mediaSession;
        private VoiceRecognizer _voiceRecognizer;

        class MediaButtonPressedCallback : MediaSession.Callback
        {
            private readonly Func<bool> _action;

            public MediaButtonPressedCallback(Func<bool> action)
            {
                _action = action;
            }

            public override bool OnMediaButtonEvent(Intent mediaButtonIntent)
            {
                return _action();
            }
        }
        public override void OnCreate()
        {
            _mediaSession = new MediaSession(this, "Elmar");
            _mediaSession.SetFlags(MediaSessionFlags.HandlesMediaButtons | MediaSessionFlags.HandlesTransportControls);
            _mediaSession.SetCallback(new MediaButtonPressedCallback(OnMediaButtonPressed));
            _mediaSession.Active = true;

            _voiceRecognizer = new VoiceRecognizer(this);
        }

        private bool OnMediaButtonPressed()
        {
            //Toast.MakeText(this, "Key pressed", ToastLength.Short).Show();
            _voiceRecognizer.StartVoiceRecognition();
            return true;
        }

        public MediaController MediaController { get; set; }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}