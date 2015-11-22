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
            _eventAdapter = new EventAdapter(this, _eventManager.GetRegisteredEvents());
            _events.Adapter = _eventAdapter;
        }


        private class EventAdapter : BaseAdapter<Event>
        {
            private readonly Context _context;
            private readonly List<Event> _events;

            public EventAdapter(Context context, List<Event> events)
            {
                _context = context;
                _events = events;
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
                checkBox.Checked = item.Enabled;

                var eventName = convertView.FindViewById<TextView>(Resource.Id.eventName);
                eventName.Text = _context.Resources.GetString(item.NameId);

                var eventOutput = convertView.FindViewById<TextView>(Resource.Id.eventOutput);
                eventOutput.Text = item.OutputText;

                return convertView;
            }

            public override int Count => _events.Count;

            public override Event this[int position] => _events[position];
        }
    }
}