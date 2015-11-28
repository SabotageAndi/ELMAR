using System.Collections.Generic;
using System.Linq;
using elmar.droid.Database;

namespace elmar.droid.Common
{
    class EventManager
    {
        private readonly Connection _connection;
        private readonly Dictionary<EventType, Event> _events;
        public EventManager(Connection connection)
        {
            _connection = connection;
            _events = new Dictionary<EventType, Event>();
        }

        public void RegisterEvent(EventType eventType, int nameId, bool defaultEnabled, string defaultOutputText)
        {
            var existingEvent = _connection.Current.Table<Event>().Where(e => e.Id == nameId).SingleOrDefault();

            if (existingEvent == null)
            {
                existingEvent = new Event(nameId);
                existingEvent.Enabled = defaultEnabled;
                existingEvent.OutputText = defaultOutputText;

                _connection.Current.Insert(existingEvent);
            }

            _events.Add(eventType, existingEvent);
        }

        public List<Event> GetRegisteredEvents()
        {
            return _events.Values.ToList();
        }

        public Event GetEvent(EventType eventType)
        {
            return _events[eventType];
        }

        public bool EventIsEnabled(EventType eventType)
        {
            var @event = GetEvent(eventType);

            return @event.Enabled;
        }

        public void Save(Event changedEvent)
        {
            var rowsAffected = _connection.Current.Update(changedEvent);
        }
    }
}