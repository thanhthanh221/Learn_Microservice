import {FaTrashAlt} from "react-icons/fa"
const LineItem = ({ item, handlerChecked, handlerDelete }) => {
    return (
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
    )
}
export default LineItem;