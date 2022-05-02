const SearchItem = ({ search, setSearch }) => {
    return (
        <form className="SearchForm">
            <label htmlFor=""></label>
            <input 
                required
                placeholder="Tìm kiếm sản phẩm"
                type="text"
                value={search}
                onChange = {(e) => setSearch(e.target.value)}
            />
        </form>
    )
}
export default SearchItem;