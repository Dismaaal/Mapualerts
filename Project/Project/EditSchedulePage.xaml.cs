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
    public partial class EditSchedulePage : ContentPage
    {
        ObservableCollection<Schedule> OSubject = new ObservableCollection<Schedule>();
        
        ParseUser currentUser = ParseUser.CurrentUser;
        string ID;
        DateTime datesel;
        public EditSchedulePage(DateTime selDate, string classId)
        {
            InitializeComponent();
            ID = classId;
            datesel = selDate;
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            Refresh();

        }
        

        public void Refresh()
        {
            var q = ParseObject.GetQuery("CalendarEvents").WhereEqualTo("Class", ID).WhereGreaterThan("EventDate", datesel).WhereLessThan("EventDate", datesel.AddDays(1));
            IEnumerable<ParseObject> events = q.FindAsync().Result;
            List<Schedule> lsched = new List<Schedule>();
            if (events.Count() != 0)
            {
                foreach (ParseObject ev in events)
                {
                    string eventType = "";
                    string cl = "";
                    string description = ev.Get<string>("EventDescription");
                    DateTime time = ev.Get<DateTime>("EventDate");

                    var qClass = ParseObject.GetQuery("Class").WhereEqualTo("objectId", ID);
                    IEnumerable<ParseObject> classes = qClass.FindAsync().Result;

                    foreach (ParseObject c in classes)
                    {
                        var qCourse = ParseObject.GetQuery("Course").WhereEqualTo("objectId", c.Get<string>("Course"));
                        IEnumerable<ParseObject> courses = qCourse.FindAsync().Result;

                        foreach (ParseObject cr in courses)
                        {
                            cl = cr.Get<string>("CourseCode") + "-" + c.Get<string>("Section");
                        }
                    }

                    var qEvent = ParseObject.GetQuery("Events").WhereEqualTo("objectId", ev.Get<string>("EventType"));
                    IEnumerable<ParseObject> eTypes = qEvent.FindAsync().Result;

                    foreach (ParseObject eType in eTypes)
                    {
                        eventType = eType.Get<string>("Event").ToString();
                    }
                    lsched.Add(new Schedule
                    {
                        type = eventType,
                        desc = description,
                        date = time,
                        id = ev.ObjectId
                    });
                    SubjectsLV.ItemsSource = OSubject;
                    foreach (Schedule s in lsched)
                    {
                        OSubject.Add(s);
                    }



                    // display += cl + "\nEvent: " + eventType + "\nDescription:" + description + "\nTime" + time + "\n\n";
                }


            }

            else
            {
               // display = "There are no events on this date";
            }

           // DisplayAlert("Events", display, "Ok");

        }
        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Schedule ann = (Schedule)SubjectsLV.SelectedItem;
            //Navigation.PushAsync(new EditEventPage(ann.id, datesel, ann.desc));
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

    }
}
