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
        public AnnouncementsPage(string classId)
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            ParseUser currentUser = ParseUser.CurrentUser;

            var q = ParseObject.GetQuery("Announcement").WhereEqualTo("Class", classId);
            IEnumerable<ParseObject> announcements = q.FindAsync().Result;

            ObservableCollection<Announcements> OAnnouncments = new ObservableCollection<Announcements>();
            List<Announcements> announce = new List<Announcements>();

            foreach (ParseObject announcement in announcements)
            {
                string subj = announcement.Get<string>("Subject");
                string mess = announcement.Get<string>("Message");
                announce.Add(new Announcements
                {
                    Subject = subj,
                    Announce = mess
                });


            }

            AnnLV.ItemsSource = OAnnouncments;
            foreach (Announcements ann in announce)
            {
                OAnnouncments.Add(ann);
            }

            AnnLV.ItemTapped += SubjectsLV_ItemTapped;
        }

        private void SubjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("Info", "Message", "OK");
        }

        private void btnEvents_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SchedulePage());
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
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
}
