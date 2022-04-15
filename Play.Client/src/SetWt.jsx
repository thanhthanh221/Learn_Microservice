import { useEffect, useState } from "react";

function B1() {
    const [value, setvalue] = useState(window.innerWidth);

    useEffect(() => {
        const handleResize = () => {
            setvalue(window.innerWidth);
        }
        window.addEventListener("resize", handleResize);
        return window.removeEventListener("reset", handleResize);       
    },[]);

    return (
        <div onChange={() => setvalue(window.innerWidth)}>
            <p>{value}</p>
        </div>
    )
}
export default B1;