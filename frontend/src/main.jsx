import { createRoot } from 'react-dom/client'
import './global.css'
import Header from './components/Header'
import MainPage from './components/MainPage'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import CartPage from './components/CartPage'
import Footer from './components/Footer'

createRoot(document.getElementById("root")).render(
  <BrowserRouter>
    <div className='min-h-screen flex flex-col'>
      <Header />
      <main className='flex-1'>
        <Routes>
          <Route path="/" element={<MainPage />}></Route>
          <Route path="/cart" element={<CartPage />}></Route>
        </Routes>
      </main>
      <Footer />
    </div>
  </BrowserRouter>
);
