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
using TinyIoC;

namespace elmar.droid.Common
{
    public static class Container
    {
        public static T Resolve<T>() where T : class
        {
            return TinyIoCContainer.Current.Resolve<T>();
        }

        public static TinyIoCContainer.RegisterOptions Register<T>() where T : class
        {
            return TinyIoCContainer.Current.Register<T>();
        }

        internal static void Register<T>(T instance) where T : class
        {
            TinyIoCContainer.Current.Register(instance);
        }
    }
}