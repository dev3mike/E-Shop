using EShop.Cart.API.Entities;
using EShop.Cart.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EShop.Cart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        [HttpGet("{userId}", Name = "GetUserCartItems")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShoppingCartItem>))]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> GetUserCartItems(int userId)
        {
            var cart = await _cartRepository.GetCart(userId);
            var cartItems = cart?.Items ?? new List<ShoppingCartItem>();
            return Ok(cartItems);
        }

        [HttpPost("{userId}", Name = "UpdateUserCartItems")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShoppingCartItem>))]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> UpdateUserCartItems(int userId, [FromBody] List<ShoppingCartItem> cartItems)
        {
            var cart = await _cartRepository.GetCart(userId);

            if (cart == null)
                cart = new ShoppingCart(userId);

            cart.Items = cartItems;
            cart.UpdatedDate = DateTime.UtcNow;

            var updatedCart = await _cartRepository.UpdateCart(cart);

            return Ok(updatedCart);
        }

        [HttpDelete("{userId}", Name = "DeleteUserCart")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<ShoppingCartItem>>> DeleteUserCart(int userId)
        {
            await _cartRepository.DeleteCart(userId);

            return NoContent();
        }
    }
}
