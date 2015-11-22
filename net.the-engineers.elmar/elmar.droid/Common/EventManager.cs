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

namespace elmar.droid.Common
{
    class EventManager
    {
        private readonly Dictionary<EventType, Event> _events;
        public EventManager()
        {
            _events = new Dictionary<EventType, Event>();
        }

        public void RegisterEvent(EventType eventType, int nameId, bool defaultEnabled, string defaultOutputText)
        {
            var @event = new Event(nameId);
            @event.Enabled = defaultEnabled;
            @event.OutputText = defaultOutputText;

            _events.Add(eventType, @event);
        }

        public List<Event> GetRegisteredEvents()
        {
            return _events.Values.ToList();
        } 
    }
}