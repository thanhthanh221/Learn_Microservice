import {FaTrashAlt} from "react-icons/fa"
import LineItem from "./LineItem";
const ItemList = ( { items, handlerChecked, handlerDelete }) => {
    return (
        (items.map((item,index) => (
            <LineItem
                key={index} 
                item = {item}
                handlerChecked = {handlerChecked}
                handlerDelete = {handlerDelete}
            />
        )))
    )
}
export default ItemList;