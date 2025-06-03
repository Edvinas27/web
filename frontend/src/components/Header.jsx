import React from 'react'
import { Link, } from 'react-router-dom'

export default function Header() {

  return (
    <>
      <div className="h-48 border-b border-gray-200 flex flex-col items-center justify-between">
        <div className="bg-[#333333] w-full">
          <h1 className="font-light mt-2 mb-2 text-xs tracking-widest text-white text-center">
            FREE SHIPPING OVER 50$
          </h1>
        </div>
        <Link to="/">
          <button className="hover:cursor-pointer">
            <div className="mb-4 mt-4 text-5xl tracking-widest">
              DROBė
            </div>
          </button>
        </Link>
      </div>

      <div className="border-b border-gray-200 h-16 flex justify-between shadow-sm">
        <div>
          <Link to="/">
            <button className="sm:ml-16 lg:ml-64 hover:cursor-pointer text-xs tracking-widest border-r border-gray-200 pr-4 h-4 mt-4 hover:underline">
              HOME
            </button>
          </Link>
          <button className="ml-4 hover:cursor-pointer text-xs tracking-widest border-r border-gray-200 pr-4 h-4 mt-4 hover:underline">
            SHOP
          </button>
          <button className="ml-4 hover:cursor-pointer text-xs tracking-widest pr-4 h-8 mt-4 hover:underline">
            CONTACT US
          </button>
        </div>

        <div>
          <Link to="/">
            <button className="mr-4 hover:cursor-pointer h-8 mt-4 hover:scale-110 transition-transform duration-200 ease-in-out">
              <img src="/assets/heart.png" alt="Heart" className="h-4" />
            </button>
          </Link>
          <Link to="/cart">
            <button className="sm:mr-16 lg:mr-64 hover:cursor-pointer h-8 mt-4 hover:scale-110 transition-transform duration-200 ease-in-out">
              <img
                src="/assets/shopping-cart.png"
                alt="Shopping Cart"
                className="h-4"
              />
            </button>
          </Link>
        </div>
      </div>
    </>
  ); 
}