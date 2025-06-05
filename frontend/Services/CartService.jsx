const API_BASE_URL = 'https://localhost:7117/api/cart';

export const cartService = {

    async getCart()
    {
        try {
            const response = await fetch(`${API_BASE_URL}`, {
              credentials: "include",
            });

            if (!response.ok) {
              throw new Error(`Failed to fetch items: ${response.statusText}`);
            }
            const data = await response.json();
            return data;
        } catch (error) {
            console.error("Error fetching cart items:", error);
        }
    },

    async addToCart(productId, quantity = 1)
    {
        try
        {
            const requestBody = {
                productId: productId,
                quantity: quantity
            }
            const response = await fetch(`${API_BASE_URL}/items`, {
              method: "POST",
              credentials: "include",
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify(requestBody)
            });

            if (!response.ok)
            {
                throw new Error(`Failed to add item: ${response.statusText}`);
            }

            const data = await response.json();
            console.log(data);

            return data;


        } catch (error)
        {
            console.error("Error adding item to cart:", error);
        }
    }
}