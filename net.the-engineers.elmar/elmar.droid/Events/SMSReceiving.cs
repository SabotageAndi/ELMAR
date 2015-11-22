using Android;
using Android.App;
using Android.Content;
using Android.Provider;
using elmar.droid.Common;

namespace elmar.droid.Events
{
    [BroadcastReceiver(Permission = Manifest.Permission.BroadcastSms)]
    [IntentFilter(new[] { Telephony.Sms.Intents.SmsReceivedAction })]
    class SMSReceivingBroadcastReceiver : StandardBroadcastReceiver
    {
        protected override EventType EventType => EventType.SMS_Receiving;
    }
}