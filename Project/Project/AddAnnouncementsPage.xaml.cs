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
        public AddAnnouncementsPage()
        {
            InitializeComponent();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            // Remove page before Edit Page
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 1]);
            // This PopAsync will now go to List Page
            this.Navigation.PopAsync();
        }
    }
}
