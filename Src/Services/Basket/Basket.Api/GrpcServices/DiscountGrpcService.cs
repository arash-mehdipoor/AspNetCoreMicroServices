using Discount.GRPC.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountService)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }


        public async Task<CouponModel> GetDiscount(string productName)
        {
            var dicountRequest = new GetRequest() { ProductName = productName };
            return await _discountService.GetAsync(dicountRequest);
        }
    }
}
