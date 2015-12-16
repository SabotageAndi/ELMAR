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
using elmar.droid.Common.Commands;
using SQLite.Net.Attributes;

namespace elmar.droid.Common
{
    enum CommandStepTypeEnum
    {
        Talk,
        Time,
        Mute,
        Unmute
    }

    class CommandStepType
    {
        public string Name { get; set; }
        public CommandStepTypeEnum Type { get; set; }

        public ICommandStepAction CommandStepAction { get; set; }
    }

    class CommandStep
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public CommandStepTypeEnum Type { get; set; }
        public int CommandId { get; set; }

        public string Parameter { get; set; }

        public int Order { get; set; }
    }
}