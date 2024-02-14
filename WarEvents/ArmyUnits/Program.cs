using ArmyUnits.Application.Commands.DeployUnits;
using ArmyUnits.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/armyUnits", (DeployUnitsRequest request) =>
{
    ArmyUnit unitToDeploy = new()
    {
        DeploymentId = Guid.NewGuid(),
        Division = request.Division,
        Mission = request.Mission,
        Quantity = request.Quantity
    };

    DeployUnitsCommand command = new();

    command.DeploySinc(unitToDeploy);
    return Results.Accepted();
})
.WithName("PostArmyUnits")
.WithOpenApi();

app.Run();
