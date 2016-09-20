using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamForms.Controls;

namespace Project
{
    public partial class SchedulePageProf : ContentPage
    {
        Calendar calendar;
        string ID;
        DateTime datesel = new DateTime();
        public SchedulePageProf(string classid)
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            ID = classid;
            var q = ParseObject.GetQuery("CalendarEvents").WhereEqualTo("Class", ID);
            IEnumerable<ParseObject> events = q.FindAsync().Result;

            List<SpecialDate> sDates = new List<SpecialDate>();

            foreach (ParseObject ev in events)
            {
                sDates.Add(new SpecialDate(ev.Get<DateTime>("EventDate")) { BackgroundColor = Color.Red, TextColor = Color.White, Selectable = true });
            }
        
            calendar = new Calendar
            {
                //MaxDate=DateTime.Now.AddDays(30), 

                SelectedDate = datesel,
                StartDate = DateTime.Now,
                //SpecialDates = new List<SpecialDate>{
                //                new SpecialDate(DateTime.Now.AddDays(2)) { BackgroundColor = Color.Red, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth=8 },
                //                new SpecialDate(DateTime.Now.AddDays(3)) { BackgroundColor = Color.Green, TextColor = Color.Blue, Selectable = true }
                //            }
                SpecialDates = sDates
            };
            Button button = new Button
            {
                Text = "Add Event",

            };
            Button btn = new Button
            {
                Text = "View Event",
            };
            Button btnUp = new Button
            {
                Text = "Edit Event",
            };

            btnUp.Clicked += BtnUp_Clicked;
            button.Clicked += OnButton_Clicked;
            btn.Clicked += Btn_Clicked;
            this.Content = new StackLayout
            {
                Children =
                        {
                            
                            calendar,
                            button,
                            btn,
                            btnUp
                        }

            };
        }

        private void BtnUp_Clicked(object sender, EventArgs e)
        {
            //datesel = (DateTime)calendar.SelectedDate;

            //string mm = datesel.Month.ToString();
            //string dd = datesel.Day.ToString();
            //string yyyy = datesel.Year.ToString();
            //string sTime = "00:00:00";
            //string compdate = dd + "/" + mm + "/" + yyyy + " " + sTime;
            //datesel = DateTime.Parse(compdate);
            //Navigation.PushAsync(new EditSchedulePage(datesel, ID));
            DisplayAlert("Note", "Not Working", "OK");

        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            string display = "";


            datesel = (DateTime)calendar.SelectedDate;

            string mm = datesel.Month.ToString();
            string dd = datesel.Day.ToString();
            string yyyy = datesel.Year.ToString();
            string sTime = "00:00:00";
            string compdate = dd + "/" + mm + "/" + yyyy + " " + sTime;
            datesel = DateTime.Parse(compdate);

            var q = ParseObject.GetQuery("CalendarEvents").WhereEqualTo("Class", ID).WhereGreaterThan("EventDate", datesel).WhereLessThan("EventDate", datesel.AddDays(1));
            IEnumerable<ParseObject> events = q.FindAsync().Result;

            if (events.Count() != 0)
            {
                foreach (ParseObject ev in events)
                {
                    string eventType = "";
                    string cl = "";
                    string description = ev.Get<string>("EventDescription");
                    string time = ev.Get<DateTime>("EventDate").TimeOfDay.ToString();

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

                    display += cl + "\nEvent: " + eventType + "\nDescription:" + description + "\nTime" + time + "\n\n";
                }
            }

            else
            {
                display = "There are no events on this date";
            }

            DisplayAlert("Events", display, "Ok");
        }

        private void OnButton_Clicked(object sender, EventArgs e)
        {
           datesel = (DateTime)calendar.SelectedDate;
            //DisplayAlert("try", datesel.ToString(), "ok");
            if (datesel > DateTime.Now)
            {
                Navigation.PushModalAsync(new AddEventPage(ID, datesel));
            }

            else
            {
                DisplayAlert("Error", "Invalid Date Selected", "OK");
            }
        }
    }
}
