import { FaPlay } from "react-icons/fa";
const Input = ({colorBg, setColorBg}) => {
    return (
        <form className="InputColor" onSubmit={(e) => e.preventDefault()}>
            <label htmlFor="">Nhập màu sắc</label>
            <input
                autoFocus
                required
                type="text"
                placeholder="Nhập Màu khu vực"
                value={colorBg}
                onChange = {(e) => (setColorBg(e.target.value))}
            />
            <button
                type="submit"
            >
                <FaPlay />
            </button>
        </form>
    )
}
export default Input;