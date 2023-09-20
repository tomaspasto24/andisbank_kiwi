using System.Text;
using System.Threading.Channels;
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

        public Boolean Send(string request)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var body = Encoding.UTF8.GetBytes(request);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "RequestQueue",
                                 basicProperties: null,
                                 body: body);
            return true;
        }

    }
}

