import React from 'react'
import { useState } from 'react'

export default function WaitingChat({ joinChatRoom }) {

    const [username, setUsername] = useState('');
    const [chatroom, setChatroom] = useState('payment');


  return (
    <div className='flex flex-col justify-center items-center h-full w-full'>
      <form onSubmit={(e) => {
        e.preventDefault();
        joinChatRoom(username, chatroom);
      }}>
        <div>
            <input
            type='text'
            value={username}
            placeholder=' Username'
            className='border rounded-s ml-2'
            onChange={(e) => setUsername(e.target.value)}
            required/>
        </div>
        <div>
            <select className='h-6 ml-8 mt-8 border rounded-s pl-2'
            value={chatroom}
            onChange={(e) => setChatroom(e.target.value)}>
                <option value='payment'>Payment Issues</option>
                <option value='general'>General Information</option>
                <option value='feedback'>Feedback</option>
                <option value='other'>Other</option>
            </select>
        </div>
        <div>
            <button type='submit' className='bg-blue-500 hover:bg-blue-600 text-white font-medium py-1 px-4 rounded mt-4 ml-22'
            disabled={!username.trim()}>Join</button>
        </div>
      </form>
    </div>
  )
}
