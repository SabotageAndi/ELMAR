namespace Elmar.Droid


open Android.App

[<Activity (Label = "Elmar.Droid", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        this.SetContentView (Resource_Layout.Main)

       



