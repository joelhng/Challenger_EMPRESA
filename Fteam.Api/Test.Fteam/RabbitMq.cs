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
            //Test de conecci�n a RabbitMQ
            RabbitConnection connection = new RabbitConnection();

            var cnn = connection.Connection();
        }
    }
}
