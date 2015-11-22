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
    class Language
    {

        public Language(string isoCode, string shortIsoCode, string name)
        {
            ShortIsoCode = shortIsoCode;
            Name = name;
            ISOCode = isoCode;
        }

        public string Name { get; }
        public string ISOCode { get;  }

        public string ShortIsoCode { get; }


        public static List<Language> All { get; } = new List<Language>() 
        {
            new Language("en-US", "en", "English (USA)")
        };

        public static Language Default => All.First();
    }
}