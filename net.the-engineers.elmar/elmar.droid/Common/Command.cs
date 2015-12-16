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
    class Command
    {
        public Command()
        {
            Steps = new List<CommandStep>();
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string CommandText { get; set; }

        [Ignore]
        public List<CommandStep> Steps { get; set; }
    }
}