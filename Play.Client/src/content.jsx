import { useState } from "react";
import { useEffect } from "react";


function Content() {
    const [value, setTitle] = useState('');
    
    // ưu tiên luồng chính giao diện người dùng
    useEffect(() => {
        document.title = value;
    })
    // Gọi sau khi gọi vào DOM
    return (
        <div>
            <input onChange={e => setTitle(e.target.value)} />

        </div>
    );
}
export default Content;

