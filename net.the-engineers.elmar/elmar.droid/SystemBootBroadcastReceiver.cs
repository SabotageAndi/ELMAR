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
using elmar.droid.Common;
using TinyIoC;

namespace elmar.droid
{
    [BroadcastReceiver]
    [IntentFilter(new []{Intent.ActionBootCompleted})]
    class SystemBootBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var voiceOutput = Container.Resolve<VoiceOutput>();
            voiceOutput.Enable(); //TODO: nicht einfach blind aktivieren

            var serviceIntent = new Intent(context, typeof(MediaButtonService));
            context.StartService(serviceIntent);
        }
    }
}