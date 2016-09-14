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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            ParseUser currentUser = ParseUser.CurrentUser;

            var q = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Student", currentUser.ObjectId);
            IEnumerable<ParseObject> assigns = q.FindAsync().Result;

            ObservableCollection<Classes> OSubject = new ObservableCollection<Classes>();
            foreach (ParseObject assign in assigns)
            {

                var qAnnouncements = ParseObject.GetQuery("Class").WhereEqualTo("objectId", assign.Get<string>("Class"));
                IEnumerable<ParseObject> cls = qAnnouncements.FindAsync().Result;
                foreach (ParseObject cl in cls)
                {
                    var qCourse = ParseObject.GetQuery("Course").WhereEqualTo("objectId", cl.Get<string>("Course"));
                    IEnumerable<ParseObject> crs = qCourse.FindAsync().Result;
                    List<Classes> subject = new List<Classes>();
                    foreach (ParseObject cr in crs)
                    {
                        string mes = cr.Get<string>("CourseCode");
                        string room = cl.Get<string>("Room");
                        subject.Add(new Classes
                        {
                            Course = mes,
                            Room = "MKT" + room
                        });
                    }

                    // OSubject = new ObservableCollection<Classes>();
                    SubjectsLV.ItemsSource = OSubject;
                    foreach (Classes s in subject)
                    {
                        OSubject.Add(s);
                    }
                }
            }

            SubjectsLV.ItemTapped += SubjectsLV_ItemTapped;

            
        }

        private async void SubjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new SchedulePage());
        }

        //SubjectsLV.ItemsSource = new List<Subject>
        //{
        //    new Subject
        //    {
        //        Mesg="IT193P",
        //        Section="BT1"
        //    },

        //    new Subject
        //    {
        //        Mesg="CS143",
        //        Section="AT1"
        //    }
        //};


        //ParseUser currentUser = ParseUser.CurrentUser;

        //if (currentUser.Get<string>("Role") == "xvcLCBmaMW")
        //{
        //    Navigation.PushAsync(new ProfessorPage());
        //}

        //var q = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Student", currentUser.ObjectId);
        //IEnumerable<ParseObject> classes = q.FindAsync().Result;

        //foreach (ParseObject cl in classes)
        //{
        //    var qAnnouncements = ParseObject.GetQuery("Announcement").WhereEqualTo("Class", cl.Get<string>("Class"));
        //    IEnumerable<ParseObject> announcements = qAnnouncements.FindAsync().Result;

        //    foreach (ParseObject announcement in announcements)
        //    {
        //        string mes = announcement.Get<string>("Message");

        //        SubjectsLV.ItemsSource = new List<Subject>
        //        {
        //            new Subject
        //            {
        //                Mesg = mes
        //            }

        //        };

        //    }
        //}
        public void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null) { return; }
            DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
          
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


        private void btnLogout_Clicked(object sender, EventArgs e)
        {

            ParseUser.LogOut();
            Navigation.PushAsync(new LoginPage());
        }

        //private async void btnViewSched_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new SchedulePage());
        //}
        private async void btnTest_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ConsultPage());
        }
    }
}
