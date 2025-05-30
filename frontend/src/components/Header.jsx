import React from 'react'
import { useState } from 'react'
import Sidebar from './subcomponents/Sidebar'
import { Link, useNavigate } from 'react-router-dom'

export default function Header() {

  const navigate = useNavigate()
  const [isOpen, setIsOpen] = useState(false)

  const toggleSidebar = () => {
    setIsOpen(!isOpen)
  }

  return (
    <div className='fixed top-0 left-0 right-0 z-50 w-screen h-24 flex justify-between items-center bg-[#BBBFCA] border-b border-white shadow-md'>
      <div className='flex flex-row items-center'>
        <button className='ml-8 h-16 w-16 hover:cursor-pointer active:scale-95'
        onClick={toggleSidebar}>
          <img src='/assets/hamburger.png'
          alt='Hamburger Icon'
          className='h-full w-full hover:invert'></img>
        </button>
        <Link to={'/'}>
        <p className='ml-16 text-5xl tracking-widest font-extralight '>DROBė<span className='text-xs tracking-normal'>since 1998</span></p>
        </Link>
      </div>
        {<Sidebar isOpen={isOpen}/>}
        <div className='flex flex-row items-center mr-8 h-12 w-32'>
            <button className='h-8 w-8 mr-4 ml-12 hover:cursor-pointer hover:invert active:scale-95'>
              <img src='/assets/heart.png'></img>
            </button>
            <button className='h-8 w-8 hover:cursor-pointer hover:invert active:scale-95'
            onClick={() => {
              navigate('/cart')
            }}>
              <img src='/assets/shopping-cart.png'></img>
            </button> 
        </div>
    </div>
  ) 
}