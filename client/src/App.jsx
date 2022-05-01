import logo from './logo.svg';
import './App.css';
import Header from './Header';
import Content from './Content';
import Footer from './Footer';
import { useState } from 'react';

function App() {
  const [items, setItem] = useState([
    {
        id: 1,
        checked : false,
        item: "Bim Bim hàn quốc"
    },
    {
        id: 2,
        checked: false,
        item: "Bim Bim hành xanh"
    },
    {
        id: 3,
        checked: false,
        item: "Bim Bim tôm"
    }
  ]);
  const handlerChecked = (id) => {
      const listItem = items.map((p) => p.id === id ? {...p, checked: !p.checked} : {...p});
      setItem(listItem);
      localStorage.setItem('shoppinglist', JSON.stringify(listItem));
  }
  const handlerDelete = (id) => {
      const listItem = items.filter((p) => p.id !== id);
      setItem(listItem);
      localStorage.setItem('shoppinglist', JSON.stringify(listItem));
  }
  
  return (
    <div className="App">
      <Header title ="Tạp hóa bán hàng"/>

      <Content
        items = {items}
        handlerChecked = {handlerChecked}
        handlerDelete = {handlerDelete}
      />

      <Footer length= {items.length} />
    </div>
  );
}

export default App;
