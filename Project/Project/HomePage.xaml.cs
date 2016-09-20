using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Project
{
    public partial class HomePage : ContentPage
    {
        string classId;
        ObservableCollection<Classes> OSubject = new ObservableCollection<Classes>();
        ParseUser currentUser = ParseUser.CurrentUser;
        public HomePage()
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
           // Refresh();
           
            SubjectsLV.ItemTapped += SubjectsLV_ItemTapped;

            
        }
        public void Refresh()
        {
            
            var q = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Student", currentUser.ObjectId);
            IEnumerable<ParseObject> assigns = q.FindAsync().Result;

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
                            Room = "MKT" + room,
                            Id = cl.ObjectId
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
            
        }

        private async void SubjectsLV_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Classes cl = (Classes)SubjectsLV.SelectedItem;
                classId = cl.Id;
                await Navigation.PushModalAsync(new TabbedPageStudent(classId));
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
        //public event EventHandler Refreshing;


        //public bool IsPullToRefreshEnabled { get; set; } = false;
        //public bool IsRefreshing { get; set; } = false;
        //public ICommand RefreshCommand { get; set; } = null;

        //public void BeginRefresh();
        //public void EndRefresh();
   

        //public class ListItem : List<string>, INotifyCollectionChanged
        //{
        //    public event NotifyCollectionChangedEventHandler CollectionChanged;

        //    public new void Reverse()
        //    {
        //        base.Reverse();

        //        if (CollectionChanged != null)
        //        {
        //            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //        }
        //    }
        //}
        protected override void OnAppearing()
        {
            Refresh();
            SubjectsLV.RefreshCommand = new Command(() =>
            {
                OSubject.Clear();
                Refresh();
                SubjectsLV.IsRefreshing = false;
            });
            
            base.OnAppearing();
        }



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
