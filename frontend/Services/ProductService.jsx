const API_BASE_URL = 'https://localhost:7117/api/products';


export const ProductService = {

    async getProducts() {
        try
        {
            const response = await fetch(`${API_BASE_URL}`, {
                method: 'GET',
            });

            if (!response.ok)
            {
                throw new Error(`Failed to fetch products: ${response.statusText}`);
            }

            const data = await response.json();

            return data || [];
        }
        catch (error)
        {
            console.error('Error fetching products:', error);
            throw error;
        }
    },

    async getProductById(productId)
    {
        try
        {
            const response = await fetch(`${API_BASE_URL}/${productId}`,
                {
                    method: 'GET',
                });

            if (!response.ok)
            {
                throw new Error(`Failed to fetch product with ID ${productId}: ${response.statusText}`);
            }

            const data = await response.json();
            
            return data || null;
        }
        catch (error)
        {
            console.error('Error fetching product by ID:', error);
            throw error;
        }
    }
}