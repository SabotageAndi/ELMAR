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
    [Activity(Label = "Commands")]
    public class CommandsActivity : Activity
    {
        private ImageButton _addCommandButton;
        private ListView _commandList;
        private CommandAdapter _commandAdapter;
        private CommandManager _commandManager;
        private CommandRepository _commandRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Commands);

            ActionBar.SetDisplayHomeAsUpEnabled(true);

            _commandManager = TinyIoCContainer.Current.Resolve<CommandManager>();
            _commandRepository = TinyIoCContainer.Current.Resolve<CommandRepository>();

            _addCommandButton = FindViewById<ImageButton>(Resource.Id.add_command);
            _addCommandButton.Click += AddCommandButtonOnClick;

            _commandAdapter = new CommandAdapter(this);
            _commandList = FindViewById<ListView>(Resource.Id.command_list);
            _commandList.Adapter = _commandAdapter;
            _commandList.ItemClick += _commandList_ItemClick;
        }

        private void _commandList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedCommand = _commandAdapter[e.Position];

            var intent = new Intent(this, typeof(CommandActivity));
            intent.PutExtra("commandId", selectedCommand.Id);

            StartActivity(intent);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        protected override void OnResume()
        {
            base.OnResume();

            var commands = _commandRepository.GetCommands();

            _commandAdapter.Update(commands);
        }

        private void AddCommandButtonOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(CommandActivity));
            StartActivity(intent);
        }

        class CommandAdapter : BaseAdapter<Command>
        {
            private readonly Context _context;
            private readonly List<Command> _commands;

            public CommandAdapter(Context context)
            {
                _context = context;
                _commands = new List<Command>();
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = convertView;

                if (view == null)
                {
                    var layoutInflater = LayoutInflater.FromContext(_context);
                    view = layoutInflater.Inflate(Resource.Layout.command_item, null);
                }

                var command = this[position];

                var commandName = view.FindViewById<TextView>(Resource.Id.commandName);
                commandName.Text = command.Name;

                var commandText = view.FindViewById<TextView>(Resource.Id.commandText);
                commandText.Text = command.CommandText;

                return view;
            }

            public override int Count => _commands.Count;

            public override Command this[int position] => _commands[position];

            public void Update(List<Command> commands)
            {
                _commands.Clear();
                _commands.AddRange(commands);
                NotifyDataSetChanged();
            }
        }
    }
}