import { useEffect, useState } from "react";

function B1() {
    const [Catalogs, SetCatalog] = useState({});
    useEffect(() => {
        fetch('https://localhost:7070/Items').then(response => response.json()).then(data => SetCatalog(data));
        console.log(SetCatalog);
    },[]);
    return (
        <div>
            <ul> {
                Catalogs.map(p => {
                    <li key={p["id"]}>{p["Description"]}</li>
                })
            }
            </ul>
        </div>
    )
}
export default B1;