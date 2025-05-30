import React from 'react'
// import { useState } from 'react'

export default function TopScreen() {

    // const [isDay, setIsDay] = useState(true);

    // const toggleDayNight = () => {
    //     setIsDay(!isDay)
    // }

  return (
    <div className='w-screen h-screen bg-[#F4F4F2] flex items-center justify-center mt-8'>
        {/* <button className='hover:cursor-pointer' onClick={toggleDayNight}>
            <div className={`w-64 h-64 rounded-full shadow-lg
                ${isDay ? 'bg-yellow-300' : 'bg-gray-800'}`}>
                {!isDay && (
                <div className='relative'>
                    <div className='absolute w-32 h-32 bg-gray-300 rounded-full left-8 top-16'></div>
                    <div className='absolute w-4 h-4 rounded-full bg-gray-400 top-24 left-12'></div>
                    <div className='absolute w-2 h-2 rounded-full bg-gray-400 top-28 left-16'></div>
                    <div className="absolute w-28 h-28 bg-gray-800 rounded-full right-16 top-16"></div>
                </div>
            )}
            </div>
        </button> */}
    </div>
  )
}
