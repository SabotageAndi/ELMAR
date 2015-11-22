using Android.App;
using Android.Content;
using elmar.droid.Common;

namespace elmar.droid.Events
{

    [BroadcastReceiver]
    [IntentFilter(new []{Intent.ActionShutdown})]
    public class ShutdownReceiver : StandardBroadcastReceiver
    {
        protected override EventType EventType => EventType.DeviceShutDown;
    }
   
}