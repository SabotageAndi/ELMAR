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
using elmar.droid.Common;
using elmar.droid.Settings;
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

            TinyIoCContainer.Current.Register(ApplicationContext);

            TinyIoCContainer.Current.Register<VoiceOutput>().AsSingleton();

            TinyIoCContainer.Current.Register<TTSChecker>().AsSingleton();
            TinyIoCContainer.Current.Register<LanguageManager>().AsSingleton();
            TinyIoCContainer.Current.Register<SettingsManager>().AsSingleton();
            TinyIoCContainer.Current.Register<EventManager>().AsSingleton();

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            var eventManager = TinyIoCContainer.Current.Resolve<EventManager>();

            eventManager.RegisterEvent(EventType.DeviceShutDown, Resource.String.ShutdownEvent, true, "Device is shutdowning");
        }
    }
}