using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class AddAnnouncementsPage : ContentPage
    {
        string cid;
        public AddAnnouncementsPage(string classid)
        {
            InitializeComponent();
            cid = classid;
           
 
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            ParseObject newAnnouncement = new ParseObject("Announcement");
            newAnnouncement["Class"] = cid.ToString();
            newAnnouncement["Subject"] = subject.Text.ToString();
            newAnnouncement["Message"] = message.Text.ToString();
            await newAnnouncement.SaveAsync();

            var q = ParseObject.GetQuery("Class");
            ParseObject cl = await q.GetAsync(cid);

            var qCr = ParseObject.GetQuery("Course");
            ParseObject cr = await qCr.GetAsync(cl.Get<string>("Course"));

            var qA = ParseObject.GetQuery("ClassAssignment").WhereEqualTo("Class", cid);
            IEnumerable<ParseObject> assigns = qA.FindAsync().Result;

            string mess = "A new Announcement has been posted in " + cr.Get<string>("CourseCode") + "-" + cl.Get<string>("Section") + " at " + DateTime.Now.ToString();

            foreach(ParseObject a in assigns)
            {
                ParseObject newNotif = new ParseObject("Notifications");
                newNotif["Class"] = cl.ObjectId;
                newNotif["Description"] = mess;
                newNotif["Type"] = "Announcement Posted";
                newNotif["Student"] = a.Get<string>("Student");
                newNotif["IsSeen"] = false;
                await newNotif.SaveAsync();
            }
            
            // Remove page before Edit Page
            //this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count]);
            // This PopAsync will now go to List Page
            await this.Navigation.PopAsync();
        }
    }
}
