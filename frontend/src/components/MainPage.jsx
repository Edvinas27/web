import { useEffect, useState } from 'react';
import { GuestTokenService } from '../../Services/GuestService';
import ItemBox from './subcomponents/ItemBox';
import { ProductService } from '../../Services/ProductService';
import { useNavigate } from 'react-router-dom';



export default function MainPage() {

  const navigate = useNavigate();

  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const initializeGuestToken = async () => {
    try {
      await GuestTokenService.getOrGenerateToken();
    } catch (error) {
      console.error("Error initializing guest token:", error);
    }
  }

  const getData = async () => {
    try {
      setLoading(true);
      setError(null);
      const response = await ProductService.getProducts();

      if(response.success)
      {
        setProducts(response.data);
      } else 
      {
        setError(response.message || "Failed to fetch products.");
      }

    } catch (error) {
      console.error("Error fetching products:", error);
      setError("An unexpected error occurred.");
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    initializeGuestToken();
    getData()
  }, []);



  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="text-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-500 mx-auto mb-4"></div>
          <h1 className="text-xl">Loading ...</h1>
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
            onClick={navigate('/')}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Try Again
          </button>
        </div>
      </div>
    );
  }

  return (  
    <div className='container mx-auto py-6 px-46 h-screen'>
      <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg-grid-cols-4 gap-6'>
        {products.map((product) => ( 
          <ItemBox key={product.id} data={product} />
        ))}
      </div>
    </div>
  );
}