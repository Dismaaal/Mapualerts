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
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace Project.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class Splashscreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Task.Delay(5000);

            // Create your application here
            StartActivity(typeof(MainActivity));
        }
        //protected override void OnStart()
        //{
        //    base.OnStart();
        //    CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        //}

        //private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        //{            
        //    if (e.IsConnected)
        //    {
                
        //    }
        //    else
        //    {
        //        //MainPage.DisplayAlert("No Network Connectivity", "This app requires a network connection", "OK");

        //    }
        //}
    }
}