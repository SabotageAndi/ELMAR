using System.Collections.Generic;
using System.Linq;
using elmar.droid.Database;

namespace elmar.droid.Common
{
    class CommandManager
    {
        private readonly Connection _connection;

        public CommandManager(Connection connection)
        {
            _connection = connection;
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
        }

        public List<Command> GetCommands()
        {
            return _connection.Current.Table<Command>().ToList();
        }

        public Command GetCommand(int commandId)
        {
            return _connection.Current.Table<Command>().Where(c => c.Id == commandId).SingleOrDefault();
        }
    }
}