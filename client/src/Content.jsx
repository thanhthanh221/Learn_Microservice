import { useState } from "react";
import {FaTrashAlt} from "react-icons/fa"

const Content = () => {
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
        <main>
            <ul>
                {items.length ?
                    (items.map((item) => (
                        <li className= "item">
                            <input
                                key={item.id}
                                type="checkbox"
                                checked = {item.checked} // check == true thì hiện
                                onChange = {() => handlerChecked(item.id)}
                            />
                            <label
                                style={(item.checked) ? {textDecoration : 'line-through' } : null}
                                onDoubleClick={() => handlerChecked(item.id)}>{item.item}</label>
                            <FaTrashAlt
                                onClick={() => handlerDelete(item.id)}
                                role= "button"
                                tabIndex = "0"
                            />
                        </li>
                    ))): <h2 style={{marginTop: "2rem"}}>Không có sản phẩm nào</h2>
                }
            </ul>
            
        </main>
    )
}
export default Content;