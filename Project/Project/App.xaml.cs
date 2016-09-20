using System.Collections.Generic;
using Parse;
using Plugin.Connectivity;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using Plugin.Connectivity.Abstractions;
using System.Threading.Tasks;

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

        public async Task WaitAndExecute(int milisec, string studId)
        {
            await Task.Delay(milisec);
            var q = ParseObject.GetQuery("Notifications").WhereEqualTo("IsSeen", false);
            IEnumerable<ParseObject> notifs = q.FindAsync().Result;

            var qAssign = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Student", studId);
            IEnumerable<ParseObject> assigns = qAssign.FindAsync().Result;

            foreach (ParseObject n in  notifs)
            {
                foreach(ParseObject a in assigns)
                {
                    if(n.Get<string>("Class") == a.Get<string>("Class") && n.Get<string>("Student") == studId)
                    {
                        Notif(n.Get<string>("Type"), n.Get<string>("Description"));

                        var qN = ParseObject.GetQuery("Notifications");
                        ParseObject noti = await qN.GetAsync(n.ObjectId);
                        noti["IsSeen"] = true;
                        await noti.SaveAsync();
                    }
                }
            }
            
        }

        protected async override void OnSleep()
        {
            //ParseSession ses = await ParseSession.GetCurrentSessionAsync();
            ParseUser user = ParseUser.CurrentUser;


            bool loop = true;
            do
            {
                await WaitAndExecute(5000, user.ObjectId);
            } while (loop == true);
        }

        public void Notif(string type, string desc)
        {
            ILocalNotification ilocalnotif = DependencyService.Get<ILocalNotification>();
            ilocalnotif.ShowNotif(type, desc);
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
