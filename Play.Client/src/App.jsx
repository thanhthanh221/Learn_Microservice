import './App.css';
import {useState} from "react";
import  Content from "./content";
import {useEffect} from "react";
import B1 from "./SetWt";

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
        check && <B1 />
      }
    </div>
  );
}

export default App;
