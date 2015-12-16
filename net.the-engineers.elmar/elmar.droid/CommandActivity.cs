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

        private ListView _stepList;
        private ImageButton _addStepButton;
        private StepAdapter _stepAdapter;

        private Command _command;
        private CommandRepository _commandRepository;
        private const int SaveId = 1;
        private const int DeleteId = 2;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Command);

            _commandManager = TinyIoCContainer.Current.Resolve<CommandManager>();
            _commandRepository = TinyIoCContainer.Current.Resolve<CommandRepository>();

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            _stepAdapter = new StepAdapter(this, _commandManager);

            _commandName = FindViewById<LinearLayout>(Resource.Id.inputCommandNameRow);
            _commandText = FindViewById<LinearLayout>(Resource.Id.inputCommandTextRow);
            _commandNameInput = FindViewById<TextView>(Resource.Id.inputCommandName);
            _commandTextInput = FindViewById<TextView>(Resource.Id.inputCommandText);
            _stepList = FindViewById<ListView>(Resource.Id.stepList);
            _stepList.Adapter = _stepAdapter;
            _addStepButton = FindViewById<ImageButton>(Resource.Id.add_step);

            var commandId = Intent.GetIntExtra("commandId", -1);

            if (commandId == -1)
            {
                _command = new Command();
            }
            else
            {
                _command = _commandRepository.GetCommand(commandId);
            }

            UpdateCommandName(_command.Name);
            UpdateCommandText(_command.CommandText);
            UpdateSteps(_command);

            _commandName.Click += CommandNameClick;
            _commandText.Click += CommandTextOnClick;
            _addStepButton.Click += AddStepButtonOnClick;
            _stepList.ItemClick += OnStepItemClick;
        }

        private void OnStepItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var step = _stepAdapter[e.Position];

            switch (step.Type)
            {
                case CommandStepTypeEnum.Talk:
                    OpenEditDialog(step.Parameter, Resource.String.OutputLanguage, (text) => { step.Parameter = text; });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnResume()
        {
            UpdateSteps(_command);
            base.OnResume();
        }

        private void AddStepButtonOnClick(object sender, EventArgs eventArgs)
        {
            var availableCommandStepTypes = _commandManager.GetAvailableCommandStepTypes();
            var commandStepTypes = availableCommandStepTypes.Select(i => i.Name).ToArray();

            AlertDialog.Builder builder = new AlertDialog.Builder(this).SetTitle(Resource.String.InputLanguage).SetItems(commandStepTypes, delegate(object o, DialogClickEventArgs args)
            {
                var stepType = availableCommandStepTypes[args.Which];

                var commandStep = _commandManager.CreateStep(_command, stepType);

                _command.Steps.Add(commandStep);
                UpdateSteps(_command);
            });

            var alertDialog = builder.Create();
            alertDialog.Show();
        }

        private void UpdateCommandText(string commandText)
        {
            _commandTextInput.Text = commandText;
        }

        private void UpdateCommandName(string name)
        {
            _commandNameInput.Text = name;
        }

        private void UpdateSteps(Command command)
        {
            _stepAdapter.Clear();
            command.Steps.ForEach(_stepAdapter.Add);
        }

        private void CommandTextOnClick(object sender, EventArgs eventArgs)
        {
            OpenEditDialog(_command.CommandText, Resource.String.CommandText, (str) =>
            {
                _command.CommandText = str;
                UpdateCommandText(str);
            });
        }

        private void CommandNameClick(object sender, EventArgs e)
        {
            OpenEditDialog(_command.Name, Resource.String.CommandName, (str) =>
            {
                _command.Name = str;
                UpdateCommandName(str);
            });
        }

        private void OpenEditDialog(string text, int titleId, Action<string> onOk)
        {
            var editText = new EditText(this);
            editText.Text = text;

            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(titleId);
            builder.SetView(editText);

            builder.SetPositiveButton(Android.Resource.String.Ok, delegate(object o, DialogClickEventArgs args) { onOk(editText.Text); });
            builder.SetNegativeButton(Android.Resource.String.Cancel, delegate(object o, DialogClickEventArgs args) { });

            builder.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var saveMenu = menu.Add(0, SaveId, 0, Resource.String.SaveCommand);
            saveMenu.SetIcon(Resource.Drawable.ic_done_white_48dp);
            saveMenu.SetShowAsAction(ShowAsAction.Always);

            var deleteMenu = menu.Add(0, DeleteId, 1, Resource.String.DeleteCommand);
            deleteMenu.SetIcon(Resource.Drawable.ic_delete_white_48dp);
            deleteMenu.SetShowAsAction(ShowAsAction.Always);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId == SaveId)
            {
                SaveCommand();
            }

            if (item.ItemId == DeleteId)
            {
                DeleteCommand();
            }

            Finish();
            return base.OnMenuItemSelected(featureId, item);
        }

        private void DeleteCommand()
        {
            _commandRepository.DeleteCommand(_command);
        }

        private void SaveCommand()
        {
            _commandRepository.Save(_command);
        }

        class StepAdapter : BaseAdapter<CommandStep>
        {
            private readonly Context _context;
            private readonly CommandManager _commandManager;
            private List<CommandStep> _steps = new List<CommandStep>();

            public StepAdapter(Context context, CommandManager commandManager)
            {
                _context = context;
                _commandManager = commandManager;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    var layoutInflater = LayoutInflater.FromContext(_context);
                    convertView = layoutInflater.Inflate(Resource.Layout.command_step_item, null);
                }

                var step = this[position];

                var stepName = convertView.FindViewById<TextView>(Resource.Id.stepName);
                var moveDownOrder = convertView.FindViewById<ImageButton>(Resource.Id.moveDownOrder);
                var moveUpOrder = convertView.FindViewById<ImageButton>(Resource.Id.moveUpOrder);

                var stepType = _commandManager.GetCommandStepType(step.Type);
                stepName.Text = stepType.Name;

                moveDownOrder.Click += (sender, args) => MoveUpOrder_Click(sender, args, position);
                moveUpOrder.Click += (sender, args) => MoveDownOrder_Click(sender, args, position);

                return convertView;
            }

            private void MoveUpOrder_Click(object sender, EventArgs e, int position)
            {
                var step = this[position];
                if (_steps.Last() == step)
                {
                    return;
                }

                var nextStep = this[position + 1];
                step.Order++;
                nextStep.Order--;

                _steps = _steps.OrderBy(i => i.Order).ToList();
                NotifyDataSetChanged();

            }

            private void MoveDownOrder_Click(object sender, EventArgs e, int position)
            {
                var step = this[position];
                if (_steps.First() == step)
                {
                    return;
                }

                var nextStep = this[position - 1];
                step.Order--;
                nextStep.Order++;

                _steps = _steps.OrderBy(i => i.Order).ToList();
                NotifyDataSetChanged();

            }

            public override int Count => _steps.Count;

            public override CommandStep this[int position] => _steps[position];

            public void Clear()
            {
                _steps.Clear();
                NotifyDataSetChanged();
            }

            public void Add(CommandStep commandStep)
            {
                _steps.Add(commandStep);
                NotifyDataSetChanged();
            }
        }
    }
}