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
    public partial class AddEventPage : ContentPage
    {
        Dictionary<string, string> Devents = new Dictionary<string, string>
        {
            {"Practical Exam", "ISWiWgDsh7" }, {"Oral Exam","3KOi0XFuaZ" }, {"Long Exam","V9QBXROhO1" }, {"Project","7UwthjWKbJ" }, {"Assignment","VkMBDQkejB" },
            { "Quiz","mKhTg0ml2G"}
        };
        string ID;
        DateTime deyt;
        string eventname;
        string eventid;
        
        public AddEventPage(string classid, DateTime date)
        {
            InitializeComponent();
            timePick.Format = "HH:mm";
            deyt = date;
            ID = classid;
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            foreach (string colorName in Devents.Keys)
            {
                picker.Items.Add(colorName);
            }
            picker.SelectedIndexChanged += (sender, args) =>
            {
                eventname = picker.Items[picker.SelectedIndex];
                eventid = Devents[eventname];
                
            };

        }
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            
            string mm = deyt.Month.ToString();
            string dd = deyt.Day.ToString();
            string yyyy = deyt.Year.ToString();
            string time = timePick.Time.ToString();
            string compdate = dd +"/" + mm + "/" + yyyy + " " + time;
            deyt = DateTime.Parse(compdate);

            //await DisplayAlert("test", deyt.ToString(), compdate);
            if(deyt > DateTime.Now)
            {
                ParseObject newevent = new ParseObject("CalendarEvents");
                newevent["Class"] = ID;
                newevent["EventType"] = eventid;
                newevent["EventDescription"] = notes.Text.ToString();
                newevent["EventDate"] = deyt;
                try
                {
                    await newevent.SaveAsync();

                    var q = ParseObject.GetQuery("Class");
                    ParseObject cl = await q.GetAsync(ID);

                    var qCr = ParseObject.GetQuery("Course");
                    ParseObject cr = await qCr.GetAsync(cl.Get<string>("Course"));

                    var qA = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Class", ID);
                    IEnumerable<ParseObject> assigns = qA.FindAsync().Result;

                    string mess = "A scheduled event has been posted in " + cr.Get<string>("CourseCode") + "-" + cl.Get<string>("Section") + " at " + DateTime.Now.ToString();

                    foreach (ParseObject a in assigns)
                    {
                        ParseObject newNotif = new ParseObject("Notifications");
                        newNotif["Class"] = cl.ObjectId;
                        newNotif["Description"] = mess;
                        newNotif["Type"] = "New Event";
                        newNotif["Student"] = a.Get<string>("Student");
                        newNotif["IsSeen"] = false;
                        await newNotif.SaveAsync();
                    }

                    await DisplayAlert("Info", "Success!", "OK");

                }

                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }
            }

            else
            {
                await DisplayAlert("Error", "Invalid Time Selected", "OK");
            }
            
        }
    }
}
