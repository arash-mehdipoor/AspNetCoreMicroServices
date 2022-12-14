using Dapper;
using Discount.GRPC.Entities;
using Npgsql;

namespace Discount.GRPC.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = @"SELECT * FROM Coupon WHERE ProductName = @ProductName";

            var coupon = await connection.QuerySingleOrDefaultAsync<Coupon>(query, new { ProductName = productName });

            if (coupon == null)
                return new Coupon() { Amount = 0, Description = "No Discount", ProductName = "No Discount" };

            return coupon;
        }

        public async Task<bool> Update(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = @$"UPDATE Coupon SET ProductName = @ProductName ,Description =@Description,Amount =@Amount) WHERE Id = @Id";

            var result = await connection.ExecuteAsync(query, 
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,Id = coupon.Id });

            if (result == 0)
                return false;
            return true;
        }

        public async Task<bool> Create(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = @"INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)";

            var result = await connection.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (result == 0)
                return false;
            return true;
        }

        public async Task<bool> Delete(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var query = @"DELETE FROM Coupon WHERE ProductName = @ProductName";

            var result = await connection.ExecuteAsync(query, new { ProductName = productName });

            if (result == 0)
                return false;
            return true;
        }

    }
}
