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
using SQLite.Net.Attributes;

namespace elmar.droid.Common
{
    public enum EventType
    {
        DeviceShutDown,
        SMS_Receiving,

    }

    class Event
    {
        public Event()
        {
        }

        public Event(int id)
        {
            Id = id;
        }

        [PrimaryKey]
        public int Id { get; set; }

        [Column("Enabled")]
        public bool Enabled { get; set; }

        [Column("OutputText")]
        public string OutputText { get; set; }  
    }
}