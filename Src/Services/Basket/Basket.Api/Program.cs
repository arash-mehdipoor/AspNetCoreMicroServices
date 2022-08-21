using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.GRPC.Protos;

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
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(op =>
{
    op.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddScoped<DiscountGrpcService>();



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
