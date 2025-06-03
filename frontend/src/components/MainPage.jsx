import { useEffect } from 'react';
import { GuestTokenService } from '../../Services/GuestService';
import Header from './Header';
import Footer from './Footer';

export default function MainPage() {

  const initializeGuestToken = async () => {
    try {
      await GuestTokenService.getOrGenerateToken();
    } catch (error) {
      console.error("Error initializing guest token:", error);
    }
  }

  useEffect(() => {
    initializeGuestToken();
  }, []);

  return (  
    <div>
    </div>
  );
}