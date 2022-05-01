const Footer = ({length}) => {
    const today = new Date();
    return (
        <footer>
            <p>{length} {length === 1 ? "item " : "items"}</p>
        </footer>
    )
}
export default Footer;