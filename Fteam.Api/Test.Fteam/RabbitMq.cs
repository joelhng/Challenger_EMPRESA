using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fteam.RabbitMQ;

namespace Test.Fteam
{
    [TestClass]
    public class RabbitMq
    {
        [TestMethod]
        public void Connection()
        {
            //Test de conección a RabbitMQ
            RabbitConnection connection = new RabbitConnection();

            var cnn = connection.Connection();
        }
    }
}
