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
    [Activity(Label = "Command")]
    public class CommandActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Command);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var menuItem = menu.Add(0, 1, 0, Resource.String.SaveCommand);
            menuItem.SetIcon(Resource.Drawable.ic_done_white_48dp);
            menuItem.SetShowAsAction(ShowAsAction.Always);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            if (item.ItemId != Android.Resource.Id.Home)
            {
                SaveCommand();
            }

            Finish();
            return base.OnMenuItemSelected(featureId, item);
        }

        private void SaveCommand()
        {

        }
    }
}