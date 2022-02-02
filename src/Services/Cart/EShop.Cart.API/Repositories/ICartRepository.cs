using EShop.Cart.API.Entities;
using System.Threading.Tasks;

namespace EShop.Cart.API.Repositories
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCart(int userId);
        Task<ShoppingCart> UpdateCart(ShoppingCart shoppingCart);
        Task DeleteCart(int userId);
    }
}
