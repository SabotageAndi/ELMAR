using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace elmar.droid.Common.Commands
{
    class MuteCommandStepAction : ICommandStepAction
    {
        private readonly Context _context;

        public MuteCommandStepAction(Context context)
        {
            _context = context;
        }

        public void Execute(string parameter)
        {
            var audioService = (AudioManager)_context.GetSystemService(Context.AudioService);
            audioService.AdjustStreamVolume(Stream.System, Adjust.Mute, VolumeNotificationFlags.RemoveSoundAndVibrate);
            audioService.AdjustStreamVolume(Stream.Alarm, Adjust.Mute, VolumeNotificationFlags.RemoveSoundAndVibrate);
            audioService.AdjustStreamVolume(Stream.Music, Adjust.Mute, VolumeNotificationFlags.RemoveSoundAndVibrate);
            audioService.AdjustStreamVolume(Stream.Notification, Adjust.Mute, VolumeNotificationFlags.RemoveSoundAndVibrate);
            audioService.AdjustStreamVolume(Stream.Ring, Adjust.Mute, VolumeNotificationFlags.RemoveSoundAndVibrate);
        }
    }
}