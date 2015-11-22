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
using elmar.droid.Common;

namespace elmar.droid.Settings
{
    class SettingsManager
    {
        private readonly Context _context;
        private readonly LanguageManager _languageManager;

        public SettingsManager(Context context, LanguageManager languageManager)
        {
            _context = context;
            _languageManager = languageManager;
        }

        public Language InputLanguage
        {
            get
            {
                var languageCode = GetSharedPreferences().GetString("inputLanguage", Language.Default.ISOCode);
                var language = _languageManager.GetByIso(languageCode);

                return language;
            }
        }

        public Language OutputLanguage
        {
            get
            {
                var languageCode = GetSharedPreferences().GetString("outputLanguage", Language.Default.ISOCode);
                var language = _languageManager.GetByIso(languageCode);

                return language;
            }
        }

        private ISharedPreferences GetSharedPreferences()
        {
            return _context.GetSharedPreferences("net.the-engineers.elmar.droid", FileCreationMode.Private);
        }
    }
}