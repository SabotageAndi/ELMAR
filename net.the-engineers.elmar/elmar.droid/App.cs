using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using elmar.droid.Voice;
using TinyIoC;

namespace elmar.droid
{
    [Application]
    class App : Application
    {
        protected App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            TinyIoCContainer.Current.Register<VoiceOutput>().AsSingleton();

            TinyIoCContainer.Current.Register<TTSChecker>().AsSingleton();
        }
    }
}