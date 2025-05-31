import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { cartService } from '../../Services/CartService'


export default function CartPage() {

    const navigate = useNavigate()
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchCartItems = async () => {
            try
            {
                setLoading(true);
                const items = await cartService.getItems();
                setData(items);
            } catch (error)
            {
                console.error('Error fetching cart items:', error);
            } finally {
                setLoading(false);
            }
        }

        fetchCartItems();
    }, []);

    const total = data.reduce((acc, item) => acc + item.price, 0);
    const tax = total * 0.07; // Assuming a 7% sales tax
    const grandTotal = total + tax;

  if(loading)
    {
        return (
            <div className='flex justify-center items-center h-screen'>
                <h1 className='text-2xl font-bold'>Loading...</h1>
            </div>
        )
    }  

  return (
     <div className='flex space-between mt-28 ml-4'>
        <table className='w-full border-collapse'>
            <tbody>
            <tr className='border-b'>
                <th className='text-left pl-4'>Item</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Total</th>
            </tr>
            {data.map((item) => (
                <tr key={item.id} className='text-center border-b '>
                    <td>{
                        <div className='flex items-center pl-4'>
                            <img className='h-32 w-32' src={item.imageUrl}/>
                            <h2 className='mb-12 ml-8 text-xl'>{item.name}</h2>
                        </div>
                        }</td>
                    <td>${item.price}</td>
                    <td>{item.quantity}</td>
                    <td>${(item.price).toFixed(2)}</td>
                </tr>
            ))}
            </tbody>
        </table>
        <div className='border-2 w-168 h-128 ml-16 flex flex-col mr-4'>
            <h1 className='text-3xl font-medium ml-8 tracking-widest text-center'>
                Your cart ({data.length} Items)
            </h1>
            <div className='mt-24'>
                <div className='flex justify-between border-b'>
                    <h2 className='text-2xl ml-2'>Subtotal:</h2>
                    <h2 className='text-xl font-light mr-4'>${total.toFixed(2)}</h2>
                </div>
                <div className='flex justify-between border-b mt-8'>
                    <h2 className='text-2xl ml-2'>Sales Tax:</h2>
                    <h2 className='text-xl font-light mr-4'>${tax.toFixed(2)}</h2>
                </div>
                <div className='flex justify-between border-b mt-8'>
                    <h2 className='text-2xl ml-2'>Coupon Code:</h2>
                    <h2 className='text-xl font-light mr-4'>null</h2>
                </div>
                <div className='flex justify-between border-b mt-16'>
                    <h2 className='text-2xl ml-2'>Grand Total:</h2>
                    <h2 className='text-xl font-light mr-4'>${grandTotal.toFixed(2)}</h2>
                </div>
            </div>
            <button className='bg-blue-500 text-white font-bold py-2 px-4 rounded mt-8 ml-8 mr-8 hover:bg-blue-600 active:scale-95 transition duration-200 hover:cursor-pointer'
            onClick={() => {
                alert('Proceeding to checkout');
                navigate('/')
            }}>
                Check Out
            </button>
        </div>
    </div>
  )
}