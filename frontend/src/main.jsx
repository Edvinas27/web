import { createRoot } from 'react-dom/client'
import './global.css'
import Header from './components/Header'
import MainPage from './components/MainPage'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import CartPage from './components/CartPage'

createRoot(document.getElementById('root')).render(
  <BrowserRouter>
    <Header></Header>
    <Routes>
      <Route path='/' element={<MainPage />}></Route>
      <Route path='/cart' element={<CartPage />}></Route>
    </Routes>
  </BrowserRouter>
)
