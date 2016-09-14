using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Project
{
    public partial class ConsultPage : ContentPage
    {

        public ConsultPage()
        {
            InitializeComponent();
            
        }
        private void btnSMS_Clicked(object sender, EventArgs e)
        {
            var SMS = MessagingPlugin.SmsMessenger;
            if (SMS.CanSendSms)
            {
                SMS.SendSms(SendTo.Text.ToString(), Msg.Text.ToString());
            }
        }

        private void btnCall_Clicked(object sender, EventArgs e)
        {
            var call = MessagingPlugin.PhoneDialer;
            if(call.CanMakePhoneCall)
            {
                call.MakePhoneCall(SendTo.Text.ToString());
            }
        }

        private void btnEmail_Clicked(object sender, EventArgs e)
        {
            var emailTask = MessagingPlugin.EmailMessenger;
            if (emailTask.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, CC, or BCC.
                emailTask.SendEmail("plugins@xamarin.com", "Xamarin Messaging Plugin", "Hello from your friends at Xamarin!");

                // Send a more complex email with the EmailMessageBuilder fluent interface.
                var email = new EmailMessageBuilder()
                  .To("plugins@xamarin.com")
                  .Cc("plugins.cc@xamarin.com")
                  .Bcc(new[] { "plugins.bcc@xamarin.com", "plugins.bcc2@xamarin.com" })
                  .Subject("Xamarin Messaging Plugin")
                  .Body("Hello from your friends at Xamarin!")
                  .Build();

                emailTask.SendEmail(email);
            }
        }
    }
}
