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
    [Activity(Label = "Events")]
    public class EventsActivity : Activity
    {
        private ListView _events;
        private EventAdapter _eventAdapter;
        private EventManager _eventManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _eventManager = Container.Resolve<EventManager>();

            SetContentView(Resource.Layout.Events);

            _events = FindViewById<ListView>(Resource.Id.event_list);
            _eventAdapter = new EventAdapter(this, _eventManager.GetRegisteredEvents(), OnEventChanged);
            _events.Adapter = _eventAdapter;
        }

        private void OnEventChanged(Event changedEvent)
        {
            _eventManager.Save(changedEvent);
        }


        private class EventAdapter : BaseAdapter<Event>
        {
            private readonly Context _context;
            private readonly List<Event> _events;
            private readonly Action<Event> _onEventChanged;

            public EventAdapter(Context context, List<Event> events, Action<Event> onEventChanged)
            {
                _context = context;
                _events = events;
                _onEventChanged = onEventChanged;
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
                    convertView = layoutInflater.Inflate(Resource.Layout.event_item, null);
                }

                var item = this[position];

                var checkBox = convertView.FindViewById<CheckBox>(Resource.Id.eventActivated);
                checkBox.Tag = position;
                checkBox.Checked = item.Enabled;
                checkBox.CheckedChange += CheckBoxOnCheckedChange;

                var eventName = convertView.FindViewById<TextView>(Resource.Id.eventName);
                eventName.Text = _context.Resources.GetString(item.Id);

                var eventOutput = convertView.FindViewById<TextView>(Resource.Id.eventOutput);
                eventOutput.Text = item.OutputText;

                return convertView;
            }

            private void CheckBoxOnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs checkedChangeEventArgs)
            {
                var checkBox = (CheckBox) sender;
                int position = (int) checkBox.Tag;
                var changedEvent = this[position];

                changedEvent.Enabled = checkedChangeEventArgs.IsChecked;

                _onEventChanged(changedEvent);
            }

            public override int Count => _events.Count;

            public override Event this[int position] => _events[position];
        }
    }
}