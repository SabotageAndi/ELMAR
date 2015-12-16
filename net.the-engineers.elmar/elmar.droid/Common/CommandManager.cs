using System.Collections.Generic;
using System.Linq;
using Android.Accounts;
using Android.Content;
using Android.Provider;
using elmar.droid.Common.Commands;

namespace elmar.droid.Common
{
    class CommandManager
    {
        private readonly Context _context;
        private readonly CommandRepository _commandRepository;
        private readonly  List<CommandStepType> _commandStepTypes = new List<CommandStepType>(); 

        private Dictionary<string, string> _systemParameters = new Dictionary<string, string>(); 

        public CommandManager(Context context, TalkCommandStepAction talkCommandStepAction, CommandRepository commandRepository)
        {
            _context = context;
            _commandRepository = commandRepository;
            _commandStepTypes.Add(new CommandStepType() {Name = "Talk", Type = CommandStepTypeEnum.Talk, CommandStepAction = talkCommandStepAction});

            AddSystemParameters();
        }

        private void AddSystemParameters()
        {
            var cursor = _context.ContentResolver.Query(ContactsContract.Profile.ContentUri, null, null, null, null);
            var columnName = cursor.GetColumnNames();

            string name = "";

            if (cursor.Count > 0)
            {
                cursor.MoveToFirst();

                name = cursor.GetString(cursor.GetColumnIndex("display_name"));
            }
           

            _systemParameters["{CurrentUser.Name}"] = name;
        }


        public List<CommandStepType> GetAvailableCommandStepTypes()
        {
            return _commandStepTypes;
        }

        public CommandStep CreateStep(Command command, CommandStepType commandStepType)
        {
            var commandStep = new CommandStep();
            commandStep.CommandId = command.Id;
            commandStep.Type = commandStepType.Type;
            commandStep.Order = command.Steps.Count;

            return commandStep;
        }

        public CommandStepType GetCommandStepType(CommandStepTypeEnum commandStepTypeEnum)
        {
            return _commandStepTypes.Where(cst => cst.Type == commandStepTypeEnum).SingleOrDefault();
        }


        public bool FindAndExecuteCommand(string commandName)
        {
            var command = FindCommand(commandName);

            if (command == null)
                return false;

            ExecuteCommand(command);

            return true;
        }

        private void ExecuteCommand(Command command)
        {
            var steps = command.Steps.OrderBy(s => s.Order);

            foreach (var step in steps)
            {
                var commandStepType = GetCommandStepType(step.Type);

                var parameter = FillInSystemParameters(step.Parameter);
                commandStepType.CommandStepAction.Execute(parameter);
            }
        }

        private string FillInSystemParameters(string parameter)
        {
            var filledParameter = parameter;

            foreach (var systemParameter in _systemParameters)
            {
                filledParameter = filledParameter.Replace(systemParameter.Key, systemParameter.Value);
            }

            return filledParameter;
        }

        private Command FindCommand(string commandName)
        {
            return _commandRepository.GetCommand(commandName);
        }
    }
}