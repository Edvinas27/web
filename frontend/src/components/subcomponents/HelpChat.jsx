import React, { useState } from 'react'
import WaitingChat from './WaitingChat';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

export default function HelpChat({isOpen}) {

    const [conn, setConn] = useState();
    const [room, setRoom] = useState('payment');
    const [user, setUser] = useState('');

    const joinChatRoom = async (username, chatroom) => {
        try {
            const conn = new HubConnectionBuilder().
            withUrl("http://localhost:5171/chatHub").
            configureLogging(LogLevel.Information).
            withAutomaticReconnect().
            build();

            conn.on("JoinSpecificChatRoom", (user, message) => {
                console.log(` ${user} MESSAGE: ${message}`);
            })

            await conn.start();
            await conn.invoke("JoinSpecificChatRoom", {
                Username: username,
                ChatRoom: chatroom
            });

            setRoom(chatroom)
            setUser(username);

            setConn(conn);

        } catch (error) {
            console.error('Error joining chat room:', error);
        }
    }

  return (
    <div className={`
      ${isOpen ? 'border-l-2 border-t-2 border-r-2 border-black h-96' : 'h-0 border-0'} 
      w-84 rounded-t-lg overflow-hidden transition-all duration-500 ease-in-out
    `}>
        <div className='border-b-2 bg-blue-300'>
            <h1 className='text-center text-lg font-medium'>Live Chat 24/7</h1>
        </div>
        <div className='bg-white h-full w-full flex flex-col'>
            {!conn ? (<WaitingChat joinChatRoom={joinChatRoom}/>) : (
                <div className='justify-center items-center h-full w-full'>
                    <div className='border-b-2 w-full text-center flex flex-row justify-between'>
                        <h1 className='text-2xl font-bold mb-2 mt-2 ml-4'>{room.toUpperCase()}</h1>
                        <button onClick={() => {
                            conn.stop();
                            setConn(null);
                        } }
                        aria-label='Close Chat'>
                            <img
                                src='/assets/close.png'
                                alt='Close Icon'
                                className='h-6 w-6 mr-4 mb-2 mt-2 hover:cursor-pointer active:scale-95'
                            />
                        </button>
                    </div>
                    <h1>Joined as {user}</h1>
                </div>
            )}
        </div>
    </div>
  )
}