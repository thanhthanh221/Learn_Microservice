import './App.css';
import {useState} from "react";
import  Content from "./content";
import {useEffect} from "react";

// TodoList
function App() {
  const [check, SetCheck] = useState(false);
  const hascode = () => {
    SetCheck(prev => {
      if(prev === false){
        return true;
      }
      return false;
    })
  }
  return (
    <div className="App">
      <button onClick={() => SetCheck(!check)}>SetUp</button>
      {
        check && <Content />
      }
    </div>
  );
}

export default App;
