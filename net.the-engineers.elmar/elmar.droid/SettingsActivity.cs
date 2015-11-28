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
using elmar.droid.Settings;

namespace elmar.droid
{
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        private SettingsManager _settingsManager;

        private TextView _inputLanguage;
        private TextView _outputLanguage;

        private LinearLayout _inputLanguageRow;
        private LinearLayout _outputLanguageRow;
        private LinearLayout _eventRow;
        private LinearLayout _commandsRow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _settingsManager = Container.Resolve<SettingsManager>();

            SetContentView(Resource.Layout.Settings);
            ActionBar.SetDisplayHomeAsUpEnabled(true);


            _inputLanguage = FindViewById<TextView>(Resource.Id.inputLanguage);
            _outputLanguage = FindViewById<TextView>(Resource.Id.outputLanguage);

            _inputLanguageRow = FindViewById<LinearLayout>(Resource.Id.inputLanguageRow);
            _outputLanguageRow = FindViewById<LinearLayout>(Resource.Id.outputLanguageRow);
            _eventRow = FindViewById<LinearLayout>(Resource.Id.eventsRow);
            _commandsRow = FindViewById<LinearLayout>(Resource.Id.commandsRow);

            UpdateInputLanguage();
            UpdateOutputLanguage();

            _inputLanguageRow.Click += (s, e) => ChooseLanguage(_settingsManager.SetInputLanguage, UpdateInputLanguage);
            _outputLanguageRow.Click += (s, e) => ChooseLanguage(_settingsManager.SetOutputLanguage, UpdateOutputLanguage);
            _eventRow.Click += OpenEvents;
            _commandsRow.Click += OpenCommands;

        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private void OpenCommands(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CommandsActivity));
            StartActivity(intent);
        }

        private void OpenEvents(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EventsActivity));
            StartActivity(intent);
        }

        private void UpdateOutputLanguage()
        {
            _outputLanguage.Text = _settingsManager.OutputLanguage.Name;
        }

        private void UpdateInputLanguage()
        {
            _inputLanguage.Text = _settingsManager.InputLanguage.Name;
        }

        private void ChooseLanguage(Action<Language> setLanguage, Action updateDisplay)
        {
            var languageNames = Language.All.Select(l => l.Name).ToArray();

            AlertDialog.Builder builder = new AlertDialog.Builder(this)
                .SetTitle(Resource.String.InputLanguage)
                .SetItems(languageNames, delegate(object o, DialogClickEventArgs args)
                {
                    var language = Language.All[args.Which];
                    setLanguage(language);
                    updateDisplay();
                });

            var alertDialog = builder.Create();
            alertDialog.Show();
        }
    }
}