using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using elmar.droid.Database;

namespace elmar.droid.Common
{
    class CommandRepository
    {
        private readonly Connection _connection;

        public CommandRepository(Connection connection)
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


            foreach (var step in command.Steps)
            {
                step.CommandId = command.Id;
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
                command.Steps = _connection.Current.Table<CommandStep>().Where(cs => cs.CommandId == commandId).OrderBy(cs => cs.Order).ToList();
            }

            return command;
        }

        public void DeleteCommand(Command command)
        {
            _connection.Current.Delete(command);
        }

        public Command GetCommandByName(string name)
        {
            return _connection.Current.Table<Command>().Where(c => c.Name == name).SingleOrDefault();
        }

        public Command GetCommand(string commandText)
        {
            var command = _connection.Current.Table<Command>().ToList().Where(c => String.Compare(c.CommandText, commandText, StringComparison.InvariantCultureIgnoreCase) == 0).SingleOrDefault();

            if (command == null)
                return null;

            return GetCommand(command.Id);
        }
    }
}