using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class EditAnnouncementsPage : ContentPage
    {
        string subj;
        string mes;
        string cls;
        string cid;
        public EditAnnouncementsPage(string classid)
        {
            InitializeComponent();
            cid = classid;
            Retrieve();
        }
        public void Retrieve()
        {
            var q = ParseObject.GetQuery("Announcement").WhereEqualTo("objectId", cid);
            IEnumerable<ParseObject> assigns = q.FindAsync().Result;

            foreach(ParseObject item in assigns)
            {
                cls = item.Get<string>("Class");
                subj = item.Get<string>("Subject");
                mes = item.Get<string>("Message");
                
                
            }
            subject.Text = subj;
            message.Text = mes;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Announcement");
            ParseObject ann = await query.GetAsync(cid);

            ann["Class"] = cls;
            ann["Subject"] = subject.Text.ToString();
            ann["Message"] = message.Text.ToString();
            await ann.SaveAsync();
            // Remove page before Edit Page
            //this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count]);
            // This PopAsync will now go to List Page
            await this.Navigation.PopAsync();
        }
    }
}
