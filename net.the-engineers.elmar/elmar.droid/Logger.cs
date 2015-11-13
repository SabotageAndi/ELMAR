using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace elmar.droid
{
    class Logger
    {
        public static void LogCall([CallerMemberName]string methodname = "")
        {
            Log.Debug(Constants.LogTag, methodname + " called");
        }
    }
}