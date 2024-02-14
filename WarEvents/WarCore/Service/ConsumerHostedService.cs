
using WarCore.Application.Consumers.UnitsDeployed;

namespace WarCore.Service;

public class ConsumerHostedService : BackgroundService
{
    private readonly IUnitsDeployedConsumer _unitsDeployedQuery;

    public ConsumerHostedService(IUnitsDeployedConsumer unitsDeployedQuery)
    {
        _unitsDeployedQuery = unitsDeployedQuery;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _unitsDeployedQuery.ExecuteSync();
    }
}
