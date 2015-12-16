using System.Collections.Generic;
using System.Linq;
using elmar.droid.Common.Commands;

namespace elmar.droid.Common
{
    class CommandManager
    {
        private readonly CommandRepository _commandRepository;
        private readonly  List<CommandStepType> _commandStepTypes = new List<CommandStepType>(); 

        public CommandManager(TalkCommandStepAction talkCommandStepAction, CommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
            _commandStepTypes.Add(new CommandStepType() {Name = "Talk", Type = CommandStepTypeEnum.Talk, CommandStepAction = talkCommandStepAction});
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

                commandStepType.CommandStepAction.Execute(step.Parameter);
            }
        }

        private Command FindCommand(string commandName)
        {
            return _commandRepository.GetCommand(commandName);
        }
    }
}