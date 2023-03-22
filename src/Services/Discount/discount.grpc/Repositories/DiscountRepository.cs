using Dapper;
using discount.grpc.Entities;
using Npgsql;

namespace discount.rgpc.Repositories
{
	public class DiscountRepository : IDiscountRepository
	{
		private readonly IConfiguration _configuration;
		public DiscountRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<Coupon> GetDiscount(string productName)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
				("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
			if (coupon == null)
			{
				return new Coupon { ProductName = productName, Amount = 0, Description = "No Discount Desc" };
			}
			return coupon;
		}
		public async Task<bool> CreateDiscount(Coupon coupon)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var createDiscount = await connection.ExecuteAsync(
				"INSERT INTO Coupon (ProductName, Description, Amount) VALUES(@ProductName,@Description,@Amount)",
				 new { coupon.ProductName, coupon.Description, coupon.Amount });
			if (createDiscount == 0)
				return false;
			return true;
		}
		public async Task<bool> UpdateDiscount(Coupon coupon)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var updateDiscount = await connection.ExecuteAsync(
				"UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id =@Id",
				 new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });
			if (updateDiscount == 0)
				return false;
			return true;
		}
		public async Task<bool> DeleteDiscount(string productName)
		{
			using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
			var deleteDiscount = await connection.ExecuteAsync(
				"DELETE FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
			if (deleteDiscount == 0)
				return false;
			return true;
		}




	}
}
