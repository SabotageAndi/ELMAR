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
    enum CommandStepTypeEnum
    {
        Talk
    }

    class CommandStepType
    {
        public string Name { get; set; }
        public CommandStepTypeEnum Type { get; set; }
    }

    class CommandStep
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public CommandStepTypeEnum Type { get; set; }
        public int CommandId { get; set; }

        public string Parameter { get; set; }  
    }
}