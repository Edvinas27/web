const API_BASE_URL = 'https://localhost:7117/api/Cart';

export const cartService = {

    async getCart()
    {
            const response = await fetch(`${API_BASE_URL}`, {
                credentials: 'include'
            });

            if(!response.ok)
            {
                throw new Error(`Failed to fetch items: ${response.statusText}`);
            }
            const data = await response.json();
            return data;
    },
}