using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class AnnouncementsPage : ContentPage
    {
        ObservableCollection<Announcements> OAnnouncments;
        string Id;
        public AnnouncementsPage(string classId)
        {
            InitializeComponent();
            Id = classId;
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            

            AnnLV.ItemTapped += SubjectsLV_ItemTapped;
        }
        public void Refresh()
        {
            ParseUser currentUser = ParseUser.CurrentUser;

            var q = ParseObject.GetQuery("Announcement").WhereEqualTo("Class", Id);
            IEnumerable<ParseObject> announcements = q.FindAsync().Result;

            OAnnouncments = new ObservableCollection<Announcements>();
            List<Announcements> announce = new List<Announcements>();

            foreach (ParseObject announcement in announcements)
            {
                string subj = announcement.Get<string>("Subject");
                string mess = announcement.Get<string>("Message");
                string id = announcement.ObjectId;
                string time = announcement.UpdatedAt.ToString();
                announce.Add(new Announcements
                {
                    Subject = subj,
                    Announce = mess,
                    Id = id,
                    Time = time
                });


            }

            AnnLV.ItemsSource = OAnnouncments;
            foreach (Announcements ann in announce)
            {
                OAnnouncments.Add(ann);
            }
        }
        protected override void OnAppearing()
        {
            Refresh();
            //AnnLV.RefreshCommand = new Command(() =>
            //{
            //    OAnnouncments.Clear();
            //    Refresh();
            //    AnnLV.IsRefreshing = false;
            //});

            base.OnAppearing();
        }
        void OnRefresh(object sender, EventArgs e)
        {
            OAnnouncments.Clear();
            Refresh();
            AnnLV.IsRefreshing = false;
            //make sure to end the refresh state
            
        }
        private async void SubjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Announcements ann = (Announcements)AnnLV.SelectedItem;
            await DisplayAlert(ann.Subject, ann.Announce + "\n\nPosted on " + ann.Time, "OK");
            Refresh();
        }

        private void btnEvents_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SchedulePage(Id));

        }

        private void btnConsultation_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ConsultPage());
        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = (MenuItem)sender;
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

    }
}
