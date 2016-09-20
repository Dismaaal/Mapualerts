using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class NoNetworkPage : ContentPage
    {
        public NoNetworkPage()
        {
            
            InitializeComponent();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }
        protected override void OnAppearing()
        {
            DisplayAlert("No Network Connectivity", "This app requires a network connection", "OK");
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            Content = null;
            base.OnDisappearing();
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {

            if (e.IsConnected)
            {
                Navigation.PushModalAsync(new HomePage());
            }
            else
            {
                DisplayAlert("No Network Connectivity", "This app requires a network connection", "OK"); 
                
            }

        }
    }
}
