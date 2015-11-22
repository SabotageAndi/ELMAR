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
    enum EventType
    {
        DeviceShutDown
    }

    class Event
    {
        public Event(int nameId)
        {
            NameId = nameId;
        }

        public int NameId { get;  }

        public bool Enabled { get; set; }

        public string OutputText { get; set; }  
    }
}