using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class EditEventPage : ContentPage
    {
        Dictionary<string, string> Devents = new Dictionary<string, string>
        {
            {"Practical Exam", "ISWiWgDsh7" }, {"Oral Exam","3KOi0XFuaZ" }, {"Long Exam","V9QBXROhO1" }, {"Project","7UwthjWKbJ" }, {"Assignment","VkMBDQkejB" },
            { "Quiz","mKhTg0ml2G"}
        };
        string descri;
        string ID;
        DateTime deyt;
        string eventname;
        string eventid;
        public EditEventPage(string classid, DateTime date, string desc)
        {
            InitializeComponent();

            descri = desc;
            timePick.Format = "HH:mm";
            deyt = date;
            ID = classid;
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");
            //foreach (string colorName in Devents.Keys)
            //{
            //    picker.Items.Add(colorName);
            //}
            //picker.SelectedIndexChanged += (sender, args) =>
            //{
            //    eventname = picker.Items[picker.SelectedIndex];
            //    eventid = Devents[eventname];

            //};
        }
        public void Retrieve()
        { }
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {

            string mm = deyt.Month.ToString();
            string dd = deyt.Day.ToString();
            string yyyy = deyt.Year.ToString();
            string time = timePick.Time.ToString();
            string compdate = dd + "/" + mm + "/" + yyyy + " " + time;
            deyt = DateTime.Parse(compdate);

            //await DisplayAlert("test", deyt.ToString(), compdate);
            if (deyt > DateTime.Now)
            {
                ParseQuery<ParseObject> query = ParseObject.GetQuery("CalendarEvents");
                ParseObject ann = await query.GetAsync(ID);

                ann["EventType"] = eventid;
                ann["EventDescription"] = descri;
                ann["EventDate"] = deyt;
                await ann.SaveAsync();
                // Remove page before Edit Page
                //this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count]);
                // This PopAsync will now go to List Page
                await this.Navigation.PopAsync();
              
               
            }

            else
            {
                await DisplayAlert("Error", "Invalid Time Selected", "OK");
            }

        }
    }
}
