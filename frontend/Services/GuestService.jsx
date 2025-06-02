const API_BASE_URL = 'https://localhost:7117/api/guest/token';


export const GuestTokenService = {
    async getOrGenerateToken()
    {
        try
        {
            const response = await fetch(`${API_BASE_URL}`, {
                method: 'GET',
                credentials: 'include',
            });

            if (!response.ok)
            {
                throw new Error(`Failed to fetch token: ${response.statusText}`);
            }

            const data = await response.json();
            
            return data.guestId;

        } catch (error)
        {
            console.error('Error fetching or generating guest token:', error);
            throw error;
        }
    },
}