const API_BASE_URL = 'https://localhost:7117/api/Products';

export const cartService  = {

    async getItems()
    {
            const response = await fetch(`${API_BASE_URL}`);

            if(!response.ok)
            {
                throw new Error(`Failed to fetch items: ${response.statusText}`);
            }
            return response.json();
    },
}

//WILL NEED TO GET SLUG FOR KEY