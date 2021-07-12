using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamNotification
{
    public class Notification
    {
        public Sender Sender { get; set; }
        public Recipient Recipient { get; set; }
        public string Subject { get; set; }
        public string RecipientPhone { get; set; }
        public string Message { get; set; }
    }
}
