using Android.App;
using Android.Content;
using elmar.droid.Common;

namespace elmar.droid.Events
{

    [BroadcastReceiver]
    [IntentFilter(new []{Intent.ActionShutdown})]
    public class ShutdownReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var eventManager = Container.Resolve<EventManager>();

            if (eventManager.EventIsEnabled(EventType.DeviceShutDown))
                return;

            var voiceOutput = Container.Resolve<VoiceOutput>();
            var eventData = eventManager.GetEvent(EventType.DeviceShutDown);

            voiceOutput.Speek(eventData.OutputText);
        }
    }
   
}