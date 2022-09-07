using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.GRPC.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSetting:ConnectionString");
    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
    {
        ConnectTimeout = 5000,
        ConnectRetry = 5,
        SyncTimeout = 5000,
        AbortOnConnectFail = false,
        Ssl=true,
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12
    };
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(op =>
{
    op.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddScoped<DiscountGrpcService>();

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, configure) =>
    {
        configure.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
