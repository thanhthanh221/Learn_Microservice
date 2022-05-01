import { useState } from "react";
import ItemList from "./ItemList";

const Content = ({items, handlerChecked, handlerDelete}) => {
    return (
        <main>
            <ul>
                {items.length ?
                    <ItemList 
                        items = {items}
                        handlerChecked = {handlerChecked}
                        handlerDelete = {handlerDelete}
                    />
                    : <h2 style={{marginTop: "2rem"}}>Không có sản phẩm nào</h2>
                }
            </ul>
            
        </main>
    )
}
export default Content;