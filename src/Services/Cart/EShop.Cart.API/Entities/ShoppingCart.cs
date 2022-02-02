using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Cart.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(int userId)
        {
            UserId = userId;
            Items = new List<ShoppingCartItem>();
            UpdatedDate = DateTime.UtcNow;
        }

        public int UserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public List<ShoppingCartItem> Items { get; set; }

        public decimal TotalPrice {
            get
            {
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }

        public decimal TotalQuantity {
            get
            {
                return Items.Sum(x => x.Quantity);
            }
        }
    }

    public class ShoppingCartItem
    {
        public int ProductId { get; set; }

        public string ProductTitle { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
