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

        public TimeCommandStepAction(VoiceOutput voiceOutput)
        {
            _voiceOutput = voiceOutput;
        }
        public void Execute(string parameter)
        {
            var now = DateTime.Now;
            var time = $"It is {now.Hour} {now.Minute}";

            _voiceOutput.Speek(time);
        }
    }
}