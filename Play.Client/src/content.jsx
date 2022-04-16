import { useState } from "react";
import { useEffect } from "react";


function Content() {
    const tabs = ["id","name", "description"];
    
    const [values, setTitles] = useState([]);
    const [type , setType] = useState("description");

    const [checkWt, SetCheckWt] = useState(false);

    
    // ưu tiên luồng chính giao diện người dùng
    useEffect(() => {
        fetch("https://localhost:7070/Items").then(p => p.json()).then(p => setTitles(p));

    },[type]);
    useEffect(() => {
        const  handleScroll = () => {
            SetCheckWt(window.scrollY >= 200);
        }
        window.addEventListener("scroll", handleScroll);

        return () => {
            window.removeEventListener("scroll", handleScroll);
        }
    }, []);
    
    // Khác nhau thì gọi CallBack
    return (
        <div>
            {
                tabs.map(p => (
                    <button style={type === p ? {
                        color: "#fff",
                        background : "#333"
                    }: {}} key={p} onClick= {() =>setType(p)}>{p}</button>
                ))
            }
            <input onChange={e => setType(e.target.value)} value = {type} />
            {
                values.map((p) => (
                    <li key={p.id}>{p[type]}</li>
                ))               
            }
            {
                checkWt && (
                    <button onChange={() => ReturnValue()} style={{
                        position: "fixed",
                        right : 20,
                        bottom : 20
                    }}>Quay ngược lại</button>
                )
            }
        </div>
    );
}
export default Content;

