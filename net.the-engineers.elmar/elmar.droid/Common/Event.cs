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
    class Event
    {
        public Event(string name)
        {
            Name = name;
        }

        public string Name { get;  }

        public bool Enabled { get; set; }

        public string OutputText { get; set; }  
    }
}