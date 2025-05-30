import TopScreen from './TopScreen';
import MiddleScreen from './MiddleScreen';
import HelpBar from './subcomponents/HelpBar';

export default function MainPage() {
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