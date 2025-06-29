using MassTransit;
using MessageConsumerService.Mpa.Extensions;
using MessageConsumerService.Mpa.Mpapi.Data;
using MessageProcessing.Shared.Mpa.Mpapi.Dto;
using MessageConsumerService.Mpa.Mpapi.Models;
using MessageConsumerService.Mpa.Mpapi.Services;
using MessageConsumerService.src.Mpa.Mpapi.MessageReader.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Configure(builder.Configuration.GetSection("Kestrel"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the message service
builder.Services.AddScoped<IMessageQueryService, MessageQueryService>();

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            //TODO configure RabbitMQ credentials from configuration
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("message-queue", e =>
        {
            e.ConfigureConsumer<MessageConsumer>(context);
        });
    });
});


var connectionString = builder.Configuration.GetConnectionString("MessagesDbConnection");
Console.WriteLine("DB Connection: " + connectionString);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<MessageDto, Message>();
    cfg.CreateMap<Message, MessageDto>();
});


var app = builder.Build();

app.ApplyMigrations();

// Configure the HTTP request pipeline.
// User Swagger always
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
