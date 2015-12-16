using System;
using Android.App;
using Android.Runtime;
using elmar.droid.Common;
using elmar.droid.Common.Commands;
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
            Container.Register<CommandManager>().AsSingleton();


            InitDatabase();

            RegisterEvents();
            RegisterDefaultCommands();

        }

        private void RegisterDefaultCommands()
        {
            var commandRepository = Container.Resolve<CommandRepository>();
            var commandManager = Container.Resolve<CommandManager>();

            Command welcomeCommand = commandRepository.GetCommandByName("Welcome");
            if (welcomeCommand == null)
            {
                welcomeCommand = new Command() {Name = "Welcome", CommandText = "hello"};
                var welcomeStep1 = commandManager.CreateStep(welcomeCommand, commandManager.GetCommandStepType(CommandStepTypeEnum.Talk));
                var welcomeStep2 = commandManager.CreateStep(welcomeCommand, commandManager.GetCommandStepType(CommandStepTypeEnum.Talk));

                welcomeStep1.Parameter = "hi";
                welcomeStep2.Parameter = "{CurrentUser.Name}";

                welcomeCommand.Steps.Add(welcomeStep1);
                welcomeCommand.Steps.Add(welcomeStep2);

                commandRepository.Save(welcomeCommand);
            }
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