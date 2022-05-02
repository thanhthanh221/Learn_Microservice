import {FaPlus} from "react-icons/fa"
import { useRef } from "react"
const AddItem = ({ newItem, handleSubmit, SetAddItem }) => {
    const inputref = useRef();
    return (
        <form className="AddForm" onSubmit={handleSubmit}>
            <label htmlFor="AddItem">Thêm sản phẩm</label>
            <input
                autoFocus
                id="AddItem"
                ref = {inputref}
                placeholder="Thêm Sản Phẩm"
                type="text"
                required // Không được null
                value = {newItem}
                onChange={(e) => SetAddItem(e.target.value)}
            />
            <button
                type="submit"
                area-label = "Add Item"
                onClick={() => inputref.current.focus()}
            >
                <FaPlus />
            </button>
        </form>
    )
}
export default AddItem