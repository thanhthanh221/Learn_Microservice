const Square = ({colorBg}) => {
    return (
        <section
            className = "square"
            style = {{backgroundColor: colorBg }}
        >
            <p>{colorBg ? colorBg :"Không có màu"}</p>
        </section>
    )
}
Square.defaultProps = {
    colorBg : "Không có màu sắc"
}
export default Square