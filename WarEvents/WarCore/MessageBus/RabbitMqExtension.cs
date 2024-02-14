using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using WarCore.Configs;

namespace WarCore.MessageBus
{
    public class RabbitMqExtension : IRabbitMqExtension
    {
        private readonly RabbitMqConfiguration _configuration;

        public RabbitMqExtension(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }

        public IConnection CreateChannel()
        {
            ConnectionFactory factory = new()
            {
                HostName = _configuration.HostName,
                UserName = _configuration.Username,
                Password = _configuration.Password
            };

            factory.DispatchConsumersAsync = true;

            IConnection channel = factory.CreateConnection();
            return channel;
        }
    }
}
