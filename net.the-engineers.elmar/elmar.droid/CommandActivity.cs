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
using elmar.droid.Common;
using TinyIoC;

namespace elmar.droid
{
    [Activity(Label = "Command")]
    public class CommandActivity : Activity
    {
        private LinearLayout _commandName;
        private LinearLayout _commandText;

        private CommandManager _commandManager;

        private TextView _commandNameInput;
        private TextView _commandTextInput;

        private Command _command;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Command);

            _commandManager = TinyIoCContainer.Current.Resolve<CommandManager>();

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            _commandName = FindViewById<LinearLayout>(Resource.Id.inputCommandNameRow);
            _commandText = FindViewById<LinearLayout>(Resource.Id.inputCommandTextRow);
            _commandNameInput = FindViewById<TextView>(Resource.Id.inputCommandName);
            _commandTextInput = FindViewById<TextView>(Resource.Id.inputCommandText);

            var commandId = Intent.GetIntExtra("commandId", -1);

            if (commandId == -1)
            {
                _command = new Command();
            }
            else
            {
                _command = _commandManager.GetCommand(commandId);
            }

            UpdateCommandName(_command.Name);
            UpdateCommandText(_command.CommandText);

            _commandName.Click += CommandNameClick;
            _commandText.Click += CommandTextOnClick;
        }

        private void UpdateCommandText(string commandText)
        {
            _commandTextInput.Text = commandText;
        }

        private void UpdateCommandName(string name)
        {
            _commandNameInput.Text = name;
        }

        private void CommandTextOnClick(object sender, EventArgs eventArgs)
        {
            OpenEditDialog(_command.CommandText, Resource.String.CommandText, (str) => { _command.CommandText = str; UpdateCommandText(str);  });
        }

        private void CommandNameClick(object sender, EventArgs e)
        {
            OpenEditDialog(_command.Name, Resource.String.CommandName, (str) => { _command.Name = str; UpdateCommandName(str); });
        }

        private void OpenEditDialog(string text, int titleId, Action<string> onOk)
        {
            var editText = new EditText(this);
            editText.Text = text;

            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(titleId);
            builder.SetView(editText);

            builder.SetPositiveButton(Android.Resource.String.Ok, delegate (object o, DialogClickEventArgs args)
            {
                onOk(editText.Text);
            });
            builder.SetNegativeButton(Android.Resource.String.Cancel, delegate (object o, DialogClickEventArgs args) { });

            builder.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var menuItem = menu.Add(0, 1, 0, Resource.String.SaveCommand);
            menuItem.SetIcon(Resource.Drawable.ic_done_white_48dp);
            menuItem.SetShowAsAction(ShowAsAction.Always);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId != Android.Resource.Id.Home)
            {
                SaveCommand();
            }

            Finish();
            return base.OnMenuItemSelected(featureId, item);
        }

        private void SaveCommand()
        {
            _commandManager.Save(_command);
        }
    }
}