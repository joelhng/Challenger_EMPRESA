using RabbitMQ.Client;
using System;
using System.Configuration;

namespace Fteam.RabbitMQ
{
    public class RabbitConnection
    {
        private string HostName = ConfigurationManager.AppSettings["Host"];
        private string User = ConfigurationManager.AppSettings["User"];
        private string Password = ConfigurationManager.AppSettings["Password"];
        private string Port = ConfigurationManager.AppSettings["Port"];

        public static IConnection _cnn;

        public IConnection Connection()
        {
            if (_cnn == null)
            {
                var factory = new ConnectionFactory();

                factory.HostName = HostName;
                factory.UserName = User;
                factory.Password = Password;
                factory.Port = int.Parse(Port);
                factory.AutomaticRecoveryEnabled = true;

                var cnn = factory.CreateConnection();
            }


            return _cnn;
        }

    }
}
