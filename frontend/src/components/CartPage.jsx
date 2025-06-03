import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { cartService } from "../../Services/CartService";

export default function CartPage() {
  const navigate = useNavigate();
  const [cart, setCart] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [updating, setUpdating] = useState(false);

  useEffect(() => {
    fetchCart();
  }, []);

  const fetchCart = async () => {
    try {
      setLoading(true);
      setError(null);

      const cartData = await cartService.getCart();
      setCart(cartData);
    } catch (error) {
      console.error("Error fetching cart:", error);
      setError("Failed to load cart. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const updateQuantity = async (productId, newQuantity) => {
    if (newQuantity < 1) return;

    try {
      setUpdating(true);
      // api call to add update
      await cartService.updateCartItem(productId, newQuantity);
      await fetchCart();
    } catch (error) {
      console.error("Error updating quantity:", error);
      setError("Failed to update item. Please try again.");
    } finally {
      setUpdating(false);
    }
  };

  const removeItem = async (productId) => {
    try {
      setUpdating(true);
      await cartService.removeCartItem(productId);
      await fetchCart();
    } catch (error) {
      console.error("Error removing item:", error);
      setError("Failed to remove item. Please try again.");
    } finally {
      setUpdating(false);
    }
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500 mx-auto mb-4"></div>
          <h1 className="text-xl">Loading cart...</h1>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-center">
          <h1 className="text-2xl font-bold text-red-600 mb-4">Error</h1>
          <p className="text-gray-600 mb-4">{error}</p>
          <button
            onClick={fetchCart}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Try Again
          </button>
        </div>
      </div>
    );
  }

  if (!cart?.cartItems?.length) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-center">
          <h1 className="text-2xl font-bold mb-4">Your cart is empty</h1>
          <p className="text-gray-600 mb-4">Add some items to get started!</p>
          <button
            onClick={() => navigate("/")}
            className="bg-blue-500 text-white px-6 py-2 rounded hover:bg-blue-600"
          >
            Continue Shopping
          </button>
        </div>
      </div>
    );
  }

  const subtotal = cart.cartItems.reduce(
    (total, item) => total + item.quantity * item.product.price,
    0
  );
  const tax = subtotal * 0.07;
  const grandTotal = subtotal + tax;

  return (
    <div className="min-h-screen bg-gray-50 py-8 mt-24">
      <div className="max-w-7xl mx-auto px-4">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          <div className="lg:col-span-2 bg-white rounded-lg shadow p-6">
            {cart.cartItems.map((item) => (
              <div
                key={item.product.id}
                className="flex items-center border-b border-gray-200 py-4 last:border-b-0"
              >
                <img
                  src={item.product.imageUrl}
                  alt={item.product.name}
                  className="w-20 h-20 object-cover rounded mr-4"
                />

                <div className="w-64">
                  <h3 className="font-medium text-lg">{item.product.name}</h3>
                  <p className="text-gray-600">${item.product.price}</p>
                </div>

                <div className="w-32 flex items-center space-x-2 mr-16 ">
                  <button
                    onClick={() =>
                      updateQuantity(item.product.id, item.quantity - 1)
                    }
                    disabled={updating || item.quantity <= 1}
                    className="bg-gray-200 text-gray-700 w-8 h-8 rounded hover:bg-gray-300 disabled:opacity-50 hover:cursor-pointer"
                  >
                    -
                  </button>
                  <span className="w-8 text-center">{item.quantity}</span>
                  <button
                    onClick={() =>
                      updateQuantity(item.product.id, item.quantity + 1)
                    }
                    disabled={updating}
                    className="bg-gray-200 text-gray-700 w-8 h-8 rounded hover:bg-gray-300 disabled:opacity-50 hover:cursor-pointer"
                  >
                    +
                  </button>
                </div>

                <div className="text-right mr-4 w-24">
                  <p className="font-medium">
                    ${(item.quantity * item.product.price).toFixed(2)}
                  </p>
                </div>

                <button
                  onClick={() => removeItem(item.product.id)}
                  disabled={updating}
                  className="text-red-600 hover:text-red-800 disabled:opacity-50 hover:cursor-pointer hover:underline"
                >
                  Remove
                </button>
              </div>
            ))}
          </div>

          <div className="sticky top-32 bg-white rounded-lg shadow p-6 h-fit border-2">
            <h2 className="text-xl font-bold mb-4">Order Summary</h2>

            <div className="space-y-3">
              <div className="flex justify-between">
                <span>Subtotal ({cart.cartItems.length} items)</span>
                <span>${subtotal.toFixed(2)}</span>
              </div>

              <div className="flex justify-between">
                <span>Tax</span>
                <span>${tax.toFixed(2)}</span>
              </div>

              <div className="border-t pt-3 font-bold text-lg">
                <div className="flex justify-between">
                  <span>Total</span>
                  <span>${grandTotal.toFixed(2)}</span>
                </div>
              </div>
            </div>

            <button
              className="w-full bg-blue-500 text-white py-3 rounded-lg mt-6 hover:bg-blue-600 transition duration-200 hover:cursor-pointer"
              onClick={() => {
                alert("Proceeding to checkout");
                navigate("/");
              }}
            >
              Proceed to Checkout
            </button>

            <button
              onClick={() => navigate("/")}
              className="w-full text-blue-500 py-2 mt-3 hover:underline hover:cursor-pointer"
            >
              Continue Shopping
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
