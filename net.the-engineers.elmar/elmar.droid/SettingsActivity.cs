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
using elmar.droid.Settings;

namespace elmar.droid
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        private SettingsManager _settingsManager;

        private TextView _inputLanguage;
        private TextView _outputLanguage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _settingsManager = TinyIoC.TinyIoCContainer.Current.Resolve<SettingsManager>();

            SetContentView(Resource.Layout.Settings);

            _inputLanguage = FindViewById<TextView>(Resource.Id.inputLanguage);
            _outputLanguage = FindViewById<TextView>(Resource.Id.outputLanguage);

            _inputLanguage.Text = _settingsManager.InputLanguage.Name;
            _outputLanguage.Text = _settingsManager.OutputLanguage.Name;
        }
    }
}