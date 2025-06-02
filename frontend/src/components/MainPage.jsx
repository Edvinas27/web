import TopScreen from './TopScreen';
import MiddleScreen from './MiddleScreen';
import HelpBar from './subcomponents/HelpBar';
import { useEffect } from 'react';
import { GuestTokenService } from '../../Services/GuestService';

export default function MainPage() {

  useEffect(() => {
    const initializeGuestToken = async () => {
        try
        {
          await GuestTokenService.getOrGenerateToken();
        } catch (error)
        {
          console.error('Error initializing guest token:', error);
        }
      }

    initializeGuestToken();
    console.log('Guest token initialized or already exists.');
  }, []);

  return (  
    <div className='flex flex-col relative'>
        <div className='h-screen w-full'>
            <TopScreen />
        </div>
        <div className='fixed bottom-4 right-4 z-50'>
          <HelpBar />
        </div>
    </div>
  );
}