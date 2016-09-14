using System;
using XLabs.Forms.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamForms.Controls;
using System.Windows.Input;

namespace Project
{
    public partial class SchedulePage : ContentPage
    {
        DateTime datesel = new DateTime();
        public SchedulePage()
        {
            InitializeComponent();
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            
            Label label = new Label
                    {
                        Text = "Schedule",
                        TextColor = Color.Black,
                        HorizontalOptions = LayoutOptions.Center

                    };
                        Calendar calendar = new Calendar { 
							//MaxDate=DateTime.Now.AddDays(30), 
							SelectedDate = datesel,
                            StartDate = DateTime.Now,
                            SpecialDates = new List<SpecialDate>{
                                new SpecialDate(DateTime.Now.AddDays(2)) { BackgroundColor = Color.Red, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth=8 },
                                new SpecialDate(DateTime.Now.AddDays(3)) { BackgroundColor = Color.Green, TextColor = Color.Blue, Selectable = true }
                            }
                        };
                        Button button = new Button
                        {
                            Text="Add Event",

                        };
                    button.Clicked += OnButton_Clicked;
                    this.Content = new StackLayout
                    {
                         Children =
                        {
                            label,
                            calendar,
                            button
                        }

                    };                     
             }

        private void OnButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddEventPage(datesel));
        }
    }
        
 }

