using System.Collections.Generic;
using Parse;
using Plugin.Connectivity;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using Plugin.Connectivity.Abstractions;

namespace Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            //MainPage = CrossConnectivity.Current.IsConnected ? (Page)new NavigationPage(new LoginPage()) : new NoNetworkPage();
            //NavigationPage.SetHasNavigationBar(MainPage, false);
            var navPage = new NavigationPage(new LoginPage());
            NavigationPage.SetHasNavigationBar(navPage.CurrentPage, false);
            //MainPage = navPage;
            MainPage = CrossConnectivity.Current.IsConnected ? (Page)navPage : new NoNetworkPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                MainPage.DisplayAlert("Connection", "Now Connected", "OK");
            }
            else
            {
                MainPage.DisplayAlert("No Network Connectivity", "This app requires a network connection", "OK");
                MainPage.Navigation.PushModalAsync(new NoNetworkPage());
            }
             
        }
    }
}
