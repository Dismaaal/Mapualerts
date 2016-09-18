using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class TabbedPageStudent : TabbedPage
    {
        
        public TabbedPageStudent(string classID)
        {
            InitializeComponent();
            var schedPage = new SchedulePage();
            //navigationPage.Icon = "schedule.png";
            schedPage.Title = "Schedule";

            var annPage = new AnnouncementsPage(classID);
            annPage.Title = "Announcements";

            var consultPage = new ConsultPage();
            consultPage.Title = "Consulatation";

            Children.Add(annPage);
            Children.Add(schedPage);
            Children.Add(consultPage);
        }
    }
}
