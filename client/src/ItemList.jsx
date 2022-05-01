import {FaTrashAlt} from "react-icons/fa"
import LineItem from "./LineItem";
const ItemList = ( { items, handlerChecked, handlerDelete }) => {
    return (
        (items.map((item) => (
            <LineItem  
                item = {item}
                handlerChecked = {handlerChecked}
                handlerDelete = {handlerDelete}
            />
        )))
    )
}
export default ItemList;