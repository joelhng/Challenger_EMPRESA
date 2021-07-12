using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FteamNotification.Email
{
    public interface IEmail
    {

        void Send(string subject, string body);
    }
}
