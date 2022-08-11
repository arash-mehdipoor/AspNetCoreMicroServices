namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
        public ShoppingCart()
        {

        }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public string UserName { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice = item.Price * item.Quntity;
                }
                return totalPrice;
            }
        }
    }
}
