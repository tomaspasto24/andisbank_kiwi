using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using ANDISBANCKAPI;
using ANDISBANCKAPI.Entidades;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ANDISBANK_kiwi
{
    public class ReceiveRequest
    {
        private ReceiveRequest()
        {
        }
        private static ReceiveRequest instance = null;
        public static ReceiveRequest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReceiveRequest();
                }
                return instance;
            }
        }

        public string Receive()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "RequestQueueMessage",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            var messageReceived = new ManualResetEvent(false);

            string receivedMessage = null;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                receivedMessage = Encoding.UTF8.GetString(body);
                messageReceived.Set();
            };

            channel.BasicConsume(queue: "RequestQueueMessage",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

            messageReceived.WaitOne();

            return receivedMessage;
        }


    }
}

