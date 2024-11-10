using System.Threading.Tasks.Sources;
using AuraShop.Discount.Context;
using AuraShop.Discount.Dtos.Coupons;
using Dapper;

namespace AuraShop.Discount.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly DapperContext _context;

        public DiscountService(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<DiscountCouponDto>> GetAllDiscountCouponsAsync()
        {
            var query = "select * from coupons";
            using var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<DiscountCouponDto>(query);
            return values.ToList();
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto discountCouponDto)
        {
            var query = "insert into Coupons (Code,Rate,IsActive,ExpireDate) " +
                        " values (@code,@rate,@isActive,@expireDate)";

            var parameters = new DynamicParameters();
            parameters.Add("@code", discountCouponDto.Code);
            parameters.Add("@rate", discountCouponDto.Rate);
            parameters.Add("@isActive", discountCouponDto.IsActive);
            parameters.Add("@expireDate", discountCouponDto.ExpireDate);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto discountCouponDto)
        {
            var query = "update Coupons set Code=@code,Rate=@rate,IsActive=@isActive,ExpireDate=@expireDate where id=@id";

            var parameters = new DynamicParameters();
            
            parameters.Add("@id", discountCouponDto.Id);
            parameters.Add("@code", discountCouponDto.Code);
            parameters.Add("@rate", discountCouponDto.Rate);
            parameters.Add("@isActive", discountCouponDto.IsActive);
            parameters.Add("@expireDate", discountCouponDto.ExpireDate);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            var query = $"delete from  Coupons where id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("@id", id);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

        }

        public async Task<DiscountCouponDto> GetDiscountCouponById(int id)
        {
            var query = "select * from coupons where id=@id";

            var parameters = new DynamicParameters();
            parameters.Add("@id",id);

            using var connection = _context.CreateConnection();
            var value = await connection.QueryFirstOrDefaultAsync<DiscountCouponDto>(query,parameters);
            return value;
        }
    }
}
