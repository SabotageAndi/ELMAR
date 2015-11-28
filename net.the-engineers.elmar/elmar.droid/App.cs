using System;
using Android.App;
using Android.Runtime;
using elmar.droid.Common;
using elmar.droid.Database;
using elmar.droid.Settings;
using elmar.droid.Voice;

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

            Container.Register(ApplicationContext);

            Container.Register<Connection>().AsSingleton();
            Container.Register<VoiceOutput>().AsSingleton();
            
            Container.Register<TTSChecker>().AsSingleton();
            Container.Register<LanguageManager>().AsSingleton();
            Container.Register<SettingsManager>().AsSingleton();
            Container.Register<EventManager>().AsSingleton();


            InitDatabase();

            RegisterEvents();


        }

        private void InitDatabase()
        {
            var connection = Container.Resolve<Connection>();
            connection.CreateOrUpdateSchema();
        }

        private void RegisterEvents()
        {
            var eventManager = Container.Resolve<EventManager>();

            eventManager.RegisterEvent(EventType.DeviceShutDown, Resource.String.ShutdownEvent, true, "Device is shutdowning");
            eventManager.RegisterEvent(EventType.SMS_Receiving, Resource.String.SMSReceivingEvent, true, "SMS received");
        }
    }
}