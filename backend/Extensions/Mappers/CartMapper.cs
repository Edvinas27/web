using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Cart;
using backend.Models.Products;

namespace backend.Extensions.Mappers
{
    public static class CartMapper
    {
        public static CartItem ToCartItem(this AddCartItemRequest request, long cartId)
        {
            return new CartItem
            {
                CartId = cartId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
        }

        public static CartDto ToCartDto(this Cart cart)
        {
            return new CartDto
            {
                GuestId = cart.GuestId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems.Select(data => new CartItemResponse
                {
                    Quantity = data.Quantity,
                    Product = data.Product
                }).ToList()
            };
        }

        public static AddCartItemResponse ToAddCartItemResponse(this CartItem cartItem)
        {
            return new AddCartItemResponse
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }
    }
}