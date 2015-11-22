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
    class LanguageManager
    {
        public Language GetByIso(string isoCode)
        {
            return Language.All.Where(l => l.ISOCode == isoCode).FirstOrDefault();
        }
    }
}