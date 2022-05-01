
const Header = ({title}) => {
    const headerStyle = {
        background: 'mediumblue',
        color : '#fff'
    }
    return (
        <header style={headerStyle}>
            <h1>{title}</h1>    
        </header>
            
    )
}
Header.defaultProps = {
    title: "Không có tiêu đề"
}
export default Header;