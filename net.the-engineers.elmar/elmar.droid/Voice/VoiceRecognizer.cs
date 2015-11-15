using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Util;
using Android.Views;
using Android.Widget;
using TinyIoC;

namespace elmar.droid
{
    class VoiceRecognizer : Java.Lang.Object, IRecognitionListener
    {
        private readonly Context _context;
        private readonly SpeechRecognizer _speechRecognizer;
        private readonly VoiceOutput _voiceOutput;

        public VoiceRecognizer(Context context)
        {
            _context = context;

            _voiceOutput = TinyIoCContainer.Current.Resolve<VoiceOutput>();

            _speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(_context);
            _speechRecognizer.SetRecognitionListener(this);
        }

        public void StartVoiceRecognition()
        {
            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguage, "de-AT");

            intent.PutExtra(RecognizerIntent.ExtraMaxResults, 5);
            _speechRecognizer.StartListening(intent);
        }

        public void OnBeginningOfSpeech()
        {
            Logger.LogCall();
        }

        public void OnBufferReceived(byte[] buffer)
        {
            Logger.LogCall();
        }

        public void OnEndOfSpeech()
        {
            Logger.LogCall();
        }

        public void OnError(SpeechRecognizerError error)
        {
            Logger.LogCall();
            Log.Debug(Constants.LogTag, error.ToString());
        }

        public void OnEvent(int eventType, Bundle @params)
        {
            Logger.LogCall();
        }

        public void OnPartialResults(Bundle partialResults)
        {
            Logger.LogCall();
        }

        public void OnReadyForSpeech(Bundle @params)
        {
            Logger.LogCall();
        }

        public void OnResults(Bundle results)
        {
            Logger.LogCall();
            var recognisedTexts = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            var firstText = recognisedTexts.FirstOrDefault();

            string messageText;

            if (String.IsNullOrWhiteSpace(firstText))
            {
                var text = _context.Resources.GetString(Resource.String.NotRecognized);
                messageText = text;
            }
            else
            {
                messageText = firstText;
            }
            Toast.MakeText(_context, messageText, ToastLength.Short).Show();

            _voiceOutput.Speek(messageText);

        }

        public void OnRmsChanged(float rmsdB)
        {
        }
    }
}