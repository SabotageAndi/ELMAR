using System.Collections.Generic;
using System.Linq;
using elmar.droid.Database;

namespace elmar.droid.Common
{
    class CommandManager
    {
        private readonly Connection _connection;
        private readonly  List<CommandStepType> _commandStepTypes = new List<CommandStepType>(); 

        public CommandManager(Connection connection)
        {
            _connection = connection;

            _commandStepTypes.Add(new CommandStepType() {Name = "Talk", Type = CommandStepTypeEnum.Talk});
        }

        public void Save(Command command)
        {
            if (command.Id == 0)
            {
                _connection.Current.Insert(command);
            }
            else
            {
                _connection.Current.Update(command);
            }


            foreach (var step in command.Steps)
            {
                if (step.Id == 0)
                {
                    _connection.Current.Insert(step);
                }
                else
                {
                    _connection.Current.Update(step);
                }
            }
        }

        public List<Command> GetCommands()
        {
            return _connection.Current.Table<Command>().ToList();
        }

        public Command GetCommand(int commandId)
        {
            var command = _connection.Current.Table<Command>().Where(c => c.Id == commandId).SingleOrDefault();

            if (command != null)
            {
                command.Steps = _connection.Current.Table<CommandStep>().Where(cs => cs.CommandId == commandId).ToList();
            }

            return command;
        }

        public void DeleteCommand(Command command)
        {
            _connection.Current.Delete(command);
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

            return commandStep;
        }

        public CommandStepType GetCommandStepType(CommandStepTypeEnum commandStepTypeEnum)
        {
            return _commandStepTypes.Where(cst => cst.Type == commandStepTypeEnum).SingleOrDefault();
        }
    }
}