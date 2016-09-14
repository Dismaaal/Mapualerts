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
    public partial class ProfessorPage : ContentPage
    {
        public ProfessorPage()
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            ParseUser currentUser = ParseUser.CurrentUser;
            var q = ParseObject.GetQuery("Class").WhereEqualTo("Professor", currentUser.ObjectId);
            IEnumerable<ParseObject> classes = q.FindAsync().Result;
            ObservableCollection<Classes> OSubject = new ObservableCollection<Classes>();
            foreach (ParseObject cl in classes)
            {

                var qAnnouncements = ParseObject.GetQuery("Course").WhereEqualTo("objectId", cl.Get<string>("Course"));
                IEnumerable<ParseObject> announcements = qAnnouncements.FindAsync().Result;

                List<Classes> subject = new List<Classes>();
                foreach (ParseObject announcement in announcements)
                {
                    string mes = announcement.Get<string>("CourseCode");
                    string room = cl.Get<string>("Room");
                    subject.Add(new Classes
                    {
                        Course = mes,
                        Room = "MKT"+room
                    });
                }
               
               // OSubject = new ObservableCollection<Classes>();
                ClassesLV.ItemsSource = OSubject;
                foreach (Classes s in subject)
                {
                    OSubject.Add(s);
                }

            }
            ClassesLV.ItemTapped += ClassesLV_ItemTapped;
        }

        private void ClassesLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new ClassesPage());
        }

        // }

        //private async void btnSubmit_Clicked(object sender, EventArgs e)
        //{
        //    ParseObject message = new ParseObject("Announcement");
        //    message["Class"] = "eEvICkPuTX";
        //    message["Message"] = txtMessage.Text;
        //    await message.SaveAsync(); throw new NotImplementedException();
        //}

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
           ParseUser.LogOutAsync();
           Navigation.PushAsync(new LoginPage());
           
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
