using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName = "arash",
                    FirstName = "Arash",
                    LastName = "Mehdipour",
                    EmailAddress = "Arash@gmail.com",
                    AddressLine = "Tehran",
                    Country = "Tehran",
                    TotalPrice = 150,
                    CVV = "CVV",
                    CardName = "CardName",
                    CardNumber = "CardNumber",
                    State= "State",
                    ZipCode= "ZipCode",
                    Expiration = "Expiration",
                    PaymentMethod =1,
                    CreatedBy = "11",
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = "2",
                    LastModifiedDate = DateTime.Now
                }
            };
        }
    }
}
