using RabbitMQ.Client;

namespace WarCore.MessageBus;

public interface IRabbitMqExtension
{
    IConnection CreateChannel();
}
