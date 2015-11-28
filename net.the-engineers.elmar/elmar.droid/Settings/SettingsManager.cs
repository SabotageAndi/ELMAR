using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using elmar.droid.Common;
using Refractored.Xam.Settings;
using Refractored.Xam.Settings.Abstractions;

namespace elmar.droid.Settings
{
    class SettingsManager
    {
        private readonly LanguageManager _languageManager;

        private ISettings Settings => CrossSettings.Current;

        public SettingsManager(LanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        public Language InputLanguage
        {
            get
            {
                var languageCode = Settings.GetValueOrDefault("inputLanguage", Language.Default.ISOCode);
                var language = _languageManager.GetByIso(languageCode);

                return language;
            }
        }

        public void SetInputLanguage(Language language)
        {
            Settings.AddOrUpdateValue("inputLanguage", language.ISOCode);
        }

        public Language OutputLanguage
        {
            get
            {
                var languageCode = Settings.GetValueOrDefault("outputLanguage", Language.Default.ISOCode);
                var language = _languageManager.GetByIso(languageCode);

                return language;
            }
        }

        public void SetOutputLanguage(Language language)
        {
            Settings.AddOrUpdateValue("outputLanguage", language.ISOCode);
        }
    }
}