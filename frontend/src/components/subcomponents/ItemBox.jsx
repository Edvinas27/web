import React from 'react'
import { useNavigate } from 'react-router-dom';

function ItemBox({data}) {

    const navigate = useNavigate();

    const handleClick = (productId) => {
        navigate(`/product/${productId}`, {state: {product: data}});
    }

    if(!data)
    {
        return null;
    }

  return (
    <div className="flex flex-col h-full w-full border-gray-200 border-2">
      <button className="hover:cursor-pointer hover:scale-101 transition-transform duration-200 ease-in-out"
      onClick={() => handleClick(data.id)}>
        <img src={data.images[0].url} alt={data.name}
        className="w-full h-full object-cover"/>
      </button>
    </div>
  );
}

export default ItemBox
