using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using ANDISBANCKAPI;
using ANDISBANCKAPI.Entidades;
using RabbitMQ.Client;

namespace ANDISBANK_kiwi
{
    public class SendRequest
    {
        private SendRequest()
        {
        }
        private static SendRequest instance = null;
        public static SendRequest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SendRequest();
                }
                return instance;
            }
        }

        public void Send(Loan request)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            string jsonString = JsonSerializer.Serialize(request);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "RequestQueue",
                                 basicProperties: null,
                                 body: body);
        }

    }
}

