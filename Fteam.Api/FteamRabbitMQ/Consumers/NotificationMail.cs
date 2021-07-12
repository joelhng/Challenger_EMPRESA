using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamRabbitMQ.Consumers
{
    public static class NotificationMail
    {
        internal static void Notification(BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            if (!String.IsNullOrWhiteSpace(message))
            {
                var o = (FteamNotification.Notification)JsonConvert.DeserializeObject(message);

                FteamNotification.Email.IEmail email = new FteamNotification.Email.Gmail(o.Sender.Mail, o.Recipient.Mail);

                email.Send(o.Subject, o.Message);
            }
        }

    }
}
