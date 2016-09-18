using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            ParseClient.Initialize("h0XrQqdnzNEKTOwyywt7OZL8Ax7hsm1kjgS5UrLR", "5eGpLQhox6cqHQ2azGgRnuEurCJL6EfTIgKzBsFJ");

            //if (ParseUser.CurrentUser != null)
            //{
            //    Navigation.PushAsync(new HomePage());
            //}
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            string user = txtUser.Text, pass = txtPass.Text;
            try
            {
                try
                {
                    await ParseUser.LogInAsync(user, pass);
                    goto jump1;
                }
                catch
                {
                    await DisplayAlert("Error", "Wrong Username or Password", "OK");
                    goto jump2;
                }

                
jump1:
                ParseUser currentUser = ParseUser.CurrentUser;
                if (currentUser.Get<string>("Role") == "VCllbh368D")
                {
                    Application.Current.MainPage = new HomePage();
                    // Pops all but the root Page off the navigation stack, with optional animation.
                    await Navigation.PopToRootAsync(true);
                    //await Navigation.PushAsync(new HomePage());
                }
                else if (currentUser.Get<string>("Role") == "xvcLCBmaMW")
                {
                    Application.Current.MainPage = new ProfessorPage();
                    // Pops all but the root Page off the navigation stack, with optional animation.
                    await Navigation.PopToRootAsync(true);
                }

                else
                {
                    await DisplayAlert("Error", "Wrong Username or Password", "OK");

                }
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(),"OK");
            }
        jump2:
            ;
        }
    }
}
