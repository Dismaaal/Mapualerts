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
    public partial class AnnouncementsPageProf : ContentPage
    {
        string id;
        string ID;
        ObservableCollection<Announcements> OAnnouncments;
        public AnnouncementsPageProf(string classId)
        {
            InitializeComponent();
            ID = classId;
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            AnnLV.ItemTapped += SubjectsLV_ItemTapped;
           
        }

        public void Refresh()
        {
            ParseUser currentUser = ParseUser.CurrentUser;

            var q = ParseObject.GetQuery("Announcement").WhereEqualTo("Class", ID);
            IEnumerable<ParseObject> announcements = q.FindAsync().Result;
            OAnnouncments = new ObservableCollection<Announcements>();

            List<Announcements> announce = new List<Announcements>();

            foreach (ParseObject announcement in announcements)
            {
                string subj = announcement.Get<string>("Subject");
                string mess = announcement.Get<string>("Message");
                id = announcement.ObjectId;
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
            AnnLV.RefreshCommand = new Command(() =>
            {
                OAnnouncments.Clear();
                Refresh();
                AnnLV.IsRefreshing = false;
            });

            base.OnAppearing();
        }

        private async void SubjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Announcements ann = (Announcements) AnnLV.SelectedItem;
            await DisplayAlert(ann.Subject, ann.Announce + "\n\nPosted on " + ann.Time, "OK");
            Refresh();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new AddAnnouncementsPage(ID));
        }
        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Announcements ann =(Announcements) AnnLV.SelectedItem;
            Navigation.PushAsync(new EditAnnouncementsPage(ann.Id));
            //DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Announcements ann = (Announcements)AnnLV.SelectedItem;
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Announcement");
            ParseObject announce = await query.GetAsync(ann.Id);
            await announce.DeleteAsync();
            await DisplayAlert("Info", "The item as been deleted", "OK");
            

        }
    }
}
