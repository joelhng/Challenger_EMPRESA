using System;
using System.Configuration;
using RabbitMQ.Client;

namespace RabbitMq
{
    public class RabbitConnection
    {
        private string HostName = ConfigurationManager.AppSettings["Host"];
        private string User = ConfigurationManager.AppSettings["User"];
        private string Password = ConfigurationManager.AppSettings["Password"];

        

    }
}
