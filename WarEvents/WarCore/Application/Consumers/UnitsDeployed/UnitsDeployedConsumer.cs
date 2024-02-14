using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using WarCore.MessageBus;

namespace WarCore.Application.Consumers.UnitsDeployed;

public class UnitsDeployedConsumer : IUnitsDeployedConsumer, IDisposable
{
    private readonly IModel _model;
    private readonly IConnection _connection;

    public UnitsDeployedConsumer(IRabbitMqExtension rabbitMqExtension)
    {
        _connection = rabbitMqExtension.CreateChannel();
        _model = _connection.CreateModel();

        _model.QueueDeclare(
            queue: "unitsDeploy",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public async Task ExecuteSync()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Debug.WriteLine($"\n\n **** Message received ***\n\n {message}");
            await Task.CompletedTask;
        };

        _model.BasicConsume(
            queue: "unitsDeploy",
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_model.IsOpen) _model.Close();
        if (_connection.IsOpen) _connection.Close();
    }
}
