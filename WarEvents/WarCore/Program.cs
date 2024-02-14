using WarCore.Application.Consumers.UnitsDeployed;
using WarCore.Configs;
using WarCore.MessageBus;
using WarCore.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));

builder.Services.AddSingleton<IUnitsDeployedConsumer, UnitsDeployedConsumer>();
builder.Services.AddSingleton<IRabbitMqExtension, RabbitMqExtension>();
builder.Services.AddHostedService<ConsumerHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/unitsDeployed", () =>
{
    return Results.Ok("Result");
})
.WithName("")
.WithOpenApi();

app.Run();