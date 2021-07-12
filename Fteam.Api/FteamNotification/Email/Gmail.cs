using System;
using System.Net;
using System.Net.Mail;

namespace FteamNotification.Email
{
    public class Gmail : IEmail
    {
        private string _from;
        private string _recipient;

        public Gmail(string from, string recipient)
        {
            _from = from;
            _recipient = recipient;

        }

        public void Send(string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("app.fitness.team@gmail.com", "Kiara1706-"),
                EnableSsl = true,
            };

            smtpClient.Send(_from, _recipient, subject, body);
        }
    }
}
