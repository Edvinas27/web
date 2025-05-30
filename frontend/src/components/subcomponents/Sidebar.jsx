import React from 'react';

export default function Sidebar({isOpen}) {

  const handleCategoryClick = (category) => {
    console.log(`Category clicked: ${category}`);
  }

  return (
    <div
      className={`border-r border-white absolute top-24 h-[calc(100vh-6rem)] w-128 left-0 shadow-2xl bg-white flex flex-wrap 
        ${isOpen ? 'translate-x-0' : '-translate-x-full'} transition-transform duration-500 ease-in-out`}
    >
      <div className="basis-1/2 h-1/2 bg-red-200 flex items-center justify-center hover:cursor-pointer hover:bg-red-300"
        onClick={() => handleCategoryClick('WOMEN')}>
        <h1>WOMEN</h1>
      </div>
      <div className="basis-1/2 h-1/2 bg-green-200 flex items-center justify-center hover:cursor-pointer hover:bg-green-300"
        onClick={() => handleCategoryClick('CHILDREN')}>
        <h1>CHILDREN</h1>
      </div>
      <div className="basis-1/2 h-1/2 bg-blue-200 flex items-center justify-center hover:cursor-pointer hover:bg-blue-300"
        onClick={() => handleCategoryClick('MEN')}>
        <h1 className=''>MEN</h1>
      </div>
      <div className="basis-1/2 h-1/2 bg-purple-200 flex items-center justify-center hover:cursor-pointer hover:bg-purple-300"
        onClick={() => handleCategoryClick('ACCESSORIES')}>
        <h1>ACCESSORIES</h1>
      </div>
    </div>
  );
}