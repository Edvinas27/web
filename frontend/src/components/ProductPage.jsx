import {useState, useEffect } from 'react'
import { useNavigate, useLocation } from 'react-router-dom';


function ProductPage() {

    const navigate = useNavigate();
    const location = useLocation();

    const initialProduct = location.state?.product;

    const [product, setProduct] = useState(initialProduct || null);
    const [loading, setLoading] = useState(!initialProduct);
    const [error, setError] = useState(null);


    

    useEffect(() => {
        if(!product)
        {
            setLoading(true);
            setError(null);
            setProduct(null);
            // fetch by id
        }
    }, [product])

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
              onClick={navigate("/")}
              className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
              Try Again
            </button>
          </div>
        </div>
      );
    }



  return (
    <div className='container mx-auto px-4 py-8'>
        <div className='grid grid-cols-1 md:grid-cols-2 gap-8'>
            <div className='border border-gray-300'>
                <img src={product.images[0].url} className='w-full h-full'
                alt={product.name}/>
            </div>
            <div>
                <div className='border-b border-gray-300'>
                    <h1 className='text-2xl font-light tracking-widest'>{product.name}</h1>
                    <h2 className='tracking-widest text-2xl font-light mb-8 mt-4'>{product.price} EUR</h2>
                </div>
                add more photo support
            </div>
        </div>
    </div>
  )
}

export default ProductPage
