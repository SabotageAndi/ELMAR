using System.Runtime.CompilerServices;
using Android.Util;

namespace elmar.droid.Common
{
    class Logger
    {
        public static void LogCall([CallerMemberName]string methodname = "")
        {
            Log.Debug(Constants.LogTag, methodname + " called");
        }
    }
}