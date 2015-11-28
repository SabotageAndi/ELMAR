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

namespace elmar.droid
{
    [Activity(Label = "Commands")]
    public class CommandsActivity : Activity
    {
        private ImageButton _addCommandButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Commands);

            ActionBar.SetDisplayHomeAsUpEnabled(true);


            _addCommandButton = FindViewById<ImageButton>(Resource.Id.add_command);
            _addCommandButton.Click += AddCommandButtonOnClick;
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
            }

            return base.OnMenuItemSelected(featureId, item);
        }

        private void AddCommandButtonOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(CommandActivity));
            StartActivity(intent);
        }
    }
}