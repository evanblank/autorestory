using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AutoReStory
{
    [Activity(Label = "AutoReStory", MainLauncher = false, Icon = "@drawable/icon")]
    public class SetupActivity : Activity
    {
      

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Setup);

            // Get our button from the layout resource,
            // and attach an event to it

            Button startButton = FindViewById<Button>(Resource.Id.start);

			startButton.Click += delegate {this.StartActivity(typeof(MainActivity)); };
        }
    }
}

