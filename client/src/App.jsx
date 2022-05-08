import logo from './logo.svg';
import './App.css';
import Header from './Header';
import Content from './Content';
import Footer from './Footer';
import { useState, useEffect } from 'react';
import AddItem from './AddItem';
import SearchItem from './SearchItem';
import Input from './Components/Input';
import Square from './Components/Square';


function App() {
  const [colorBg, setColorBg] = useState('');

  const [newItem, SetAddItem] = useState('');

  const [items, setItem] = useState(JSON.parse(localStorage.getItem('shoppinglist')));

  const [search, setSearch] = useState('');


  useEffect(() => {
    localStorage.setItem('shoppinglist', JSON.stringify(items));
    console.log("Render");
  }, [items]);
   // Truyền mảng rỗng chỉ gọi 1 lần
   // items thay đổi thì gọi
   // UseEf sẽ gọi sau

  const setAndSaveItems = (newItems) => {
    setItem(newItems);
    localStorage.setItem('shoppinglist', JSON.stringify(newItems));
  };

  const handlerChecked = (id) => {
    const listItem = items.map((p) => p.id === id ? {...p, checked: !p.checked} : {...p});
    setAndSaveItems(listItem)
  };

  const handlerDelete = (id) => {
    const listItems = items.filter((p) => p.id !== id);
    setAndSaveItems(listItems);
  };

  const handleAdd = (item) => {
    const id = items.length ? items[items.length -1].id + 1 : 1;
    const myNewItem = { id, checked: false, item };
    const listItems = [...items, myNewItem ];
    setAndSaveItems(listItems);
  };

  const handleSubmit = (e) => { // e chỉ là sự kiện xác định thôi
    e.preventDefault();
    // Nếu sản phẩm bằng null thì không in ra
    if(!newItem) return;

    handleAdd(newItem); 
    SetAddItem('');
  };
  
  return (
    <div className="App" id='App1' >

      <Header title ="Tạp hóa bán hàng"/>
      <Square
        colorBg={colorBg}
      />

      <Input
        colorBg = {colorBg}
        setColorBg  = {setColorBg}
      />

      <SearchItem 
        search = {search}
        setSearch = {setSearch}
      />
      
      <AddItem
        newItem = {newItem}
        handleSubmit = {handleSubmit}
        SetAddItem = {SetAddItem}
      />
      
      <Content
        items = {items.filter(item => ((item.item).toLowerCase()).includes(search.toLowerCase()))}
        handlerChecked = {handlerChecked}
        handlerDelete = {handlerDelete}
      />
      <Footer length= {items.length} />
    </div>
  );
}

export default App;
