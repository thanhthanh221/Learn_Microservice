using Play.Inventory.Service.Dtos;
namespace Play.Inventory.Service.Clients
{
    public class CataLogClient
    {
        // Có thể lấy tài nguyên từ 1 URL nào nó
        private readonly HttpClient ? httpClient;

        public CataLogClient(HttpClient? httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IReadOnlyCollection<CatalogItemDto>> GetCatalogItemsAsync()
        {
            IReadOnlyCollection<CatalogItemDto>? items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>("/Items");

            return items;
        }
    }
}