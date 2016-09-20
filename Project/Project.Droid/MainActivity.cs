using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XamForms.Controls.Droid;
using Parse;

namespace Project.Droid
{
    [Activity(Label = "Project", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity//XFormsApplicationDroid  
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            LoadApplication(new App());
            //

            // Initialize the parse client with your Application ID and Parse Server URL found on
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            //ParseClient.Initialize(new ParseClient.Configuration
            //{
            //    ApplicationId = "h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR",
            //    Server = "http://localhost:1337/parse/",

            //});
        }
    }
}

