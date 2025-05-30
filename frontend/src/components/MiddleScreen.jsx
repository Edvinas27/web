import React from 'react'

export default function MiddleScreen() {

  const items = [
    { id: 1, name: 'Item 1', price: '$10' },
    { id: 2, name: 'Item 2', price: '$20' },
    { id: 3, name: 'Item 3', price: '$30' },
    { id: 4, name: 'Item 4', price: '$40' }
  ]

  return (
    <div className='h-full w-full'>
        <h2 className='text-3xl font-bold text-center mb-8'>Featured Items</h2>
        <div className='grid grid-cols-4 gap-6 mr-4 ml-4'>
            {items.map((item) => (
                <div key={item.id}
                     className='bg-white h-64 border-2 border-black rounded-lg shadow-md hover:shadow-xl hover:cursor-pointer'>
                        <h1>{item.name} {item.price}</h1>
                </div>
            ))} 
        </div>
    </div>
  )
}
