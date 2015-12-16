using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace elmar.droid.Common.Commands
{
    class TimeCommandStepAction : ICommandStepAction
    {
        private readonly VoiceOutput _voiceOutput;
        private readonly Context _context;

        public TimeCommandStepAction(VoiceOutput voiceOutput, Context context)
        {
            _voiceOutput = voiceOutput;
            _context = context;
        }

        public void Execute(string parameter)
        {
            var now = DateTime.Now;
            var outputText = _context.GetString(Resource.String.TimeCommandText);
            var time = String.Format(outputText, now.Hour, now.Minute);

            _voiceOutput.Speek(time);
        }
    }
}