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
                Id = cart.Id,
                GuestId = cart.GuestId,
                CreatedAt = cart.CreatedAt,
                CartItems = cart.CartItems.Select(data => new CartItemResponse
                {
                    Id = data.Id,
                    Quantity = data.Quantity,
                    Product = data.Product
                }).ToList()
            };
        }

        public static AddCartItemResponse ToAddCartItemResponse(this CartItem cartItem)
        {
            return new AddCartItemResponse
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }

        public static UpdateCartItemResponse ToUpdateCartItemResponse(this CartItem cartItem)
        {
            return new UpdateCartItemResponse
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }
    }
}