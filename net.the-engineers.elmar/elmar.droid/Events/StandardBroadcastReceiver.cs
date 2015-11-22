using Android.Content;
using elmar.droid.Common;

namespace elmar.droid.Events
{
    public abstract class StandardBroadcastReceiver : BroadcastReceiver
    {
        protected abstract EventType EventType { get; }

        public override void OnReceive(Context context, Intent intent)
        {
            var eventManager = Container.Resolve<EventManager>();

            if (!eventManager.EventIsEnabled(EventType))
                return;

            var voiceOutput = Container.Resolve<VoiceOutput>();
            var eventData = eventManager.GetEvent(EventType);

            voiceOutput.Speek(eventData.OutputText);
        }
    }
}