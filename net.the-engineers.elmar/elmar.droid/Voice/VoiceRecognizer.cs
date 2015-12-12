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
using elmar.droid.Common;
using elmar.droid.Settings;
using TinyIoC;

namespace elmar.droid
{
    class VoiceRecognizer : Java.Lang.Object, IRecognitionListener
    {
        private readonly Context _context;
        private readonly SpeechRecognizer _speechRecognizer;
        private readonly VoiceOutput _voiceOutput;

        private readonly SettingsManager _settingsManager;
        private CommandManager _commandManager;

        public VoiceRecognizer(Context context)
        {
            _context = context;

            _voiceOutput = TinyIoCContainer.Current.Resolve<VoiceOutput>();
            _settingsManager = TinyIoCContainer.Current.Resolve<SettingsManager>();
            _commandManager = TinyIoCContainer.Current.Resolve<CommandManager>();

            _speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(_context);
            _speechRecognizer.SetRecognitionListener(this);
        }

        public void StartVoiceRecognition()
        {
            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguage, _settingsManager.InputLanguage.ISOCode);

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

            var notRecognizedText = _context.Resources.GetString(Resource.String.NotRecognized);

            bool playNotRecognizedText = false;

            if (String.IsNullOrWhiteSpace(firstText))
            {
                playNotRecognizedText = true;
            }
            else
            {
                var result = _commandManager.FindAndExecuteCommand(firstText);

                if (!result)
                    playNotRecognizedText = true;
            }

            if (playNotRecognizedText)
            {
                Toast.MakeText(_context, notRecognizedText, ToastLength.Short).Show();
                _voiceOutput.Speek(notRecognizedText);
            }
        }

        public void OnRmsChanged(float rmsdB)
        {
        }
    }
}