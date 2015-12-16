using System;
using System.Collections.Generic;
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

            AddCommand(commandRepository, commandManager, Resource.String.defaultWelcomeCommandName, Resource.String.defaultWelcomeCommand, new List<Tuple<CommandStepTypeEnum, string>>()
            {
                new Tuple<CommandStepTypeEnum, string>(CommandStepTypeEnum.Talk, "hi"),
                new Tuple<CommandStepTypeEnum, string>(CommandStepTypeEnum.Talk, "{CurrentUser.Name}")
            });

            AddCommand(commandRepository, commandManager, Resource.String.defaultTimeCommandName, Resource.String.defaultTimeCommand, new List<Tuple<CommandStepTypeEnum, string>>()
            {
                new Tuple<CommandStepTypeEnum, string>(CommandStepTypeEnum.Time, "")
            });
        }

        private void AddCommand(CommandRepository commandRepository, CommandManager commandManager, int commandNameId, int commandTextId, List<Tuple<CommandStepTypeEnum, string>> steps)
        {
            string commandName = this.GetString(commandNameId);
            Command command = commandRepository.GetCommandByName(commandName);
            if (command == null)
            {
                string commandText = GetString(commandTextId);
                command = new Command() { Name = commandName, CommandText = commandText };
                foreach (var step in steps)
                {
                    AddStepToCommand(commandManager, command, step.Item1, step.Item2);
                }

                commandRepository.Save(command);
            }
        }

        private CommandStep AddStepToCommand(CommandManager commandManager, Command command, CommandStepTypeEnum commandStepTypeEnum, string parameter)
        {
            var commandStep = commandManager.CreateStep(command, commandManager.GetCommandStepType(commandStepTypeEnum));
            commandStep.Parameter = parameter;

            command.Steps.Add(commandStep);

            return commandStep;
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