using ArmyUnits.Domain.Entities;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ArmyUnits.Application.Commands.DeployUnits;
public class DeployUnitsCommand
{
    public void DeploySinc(ArmyUnit unit)
    {
        ConnectionFactory factory = new() { HostName = "rabbit" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "unitsDeploy",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonContent = JsonSerializer.Serialize(unit);
        var body = Encoding.UTF8.GetBytes(jsonContent);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "unitsDeploy",
            basicProperties: null,
            body: body);
    }
}
