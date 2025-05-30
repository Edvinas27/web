import React from 'react'
import { useState } from 'react'
import HelpChat from './HelpChat';

export default function HelpBar() {
    const [isOpen, setIsOpen] = useState(false);

    const toggleHelp = () => {
        setIsOpen(!isOpen);
    }

  return (
    <>
    <HelpChat isOpen={isOpen}/>
    <div className={`w-84 h-8 mr-4 mb-2 border-2 rounded-b-lg
        ${isOpen ? '' : 'active:scale-95'}`}>
      <button className='flex justify-center hover:cursor-pointer h-full w-full'
      onClick={() => {
        toggleHelp();
      }}>
        <div className='text-s font-medium'>
            {isOpen ? (
                <img 
                src='/assets/down-arrow.png'
                alt='Minimize chat'
                className='h-6 w-12'/>
            ) : 'Chat with us!'}
        </div>
      </button>
    </div>
    </>
  )
}