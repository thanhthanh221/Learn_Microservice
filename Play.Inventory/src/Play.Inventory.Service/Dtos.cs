using System.ComponentModel.DataAnnotations;
using System.Net.Http;
namespace Play.Inventory.Service.Dtos
{
    // Tạo Sản phẩm mới
    public record GrantItemDto(Guid UserId, Guid CatalogItemId, [Range(1,10000)]int Quantity);
    // Trả về khi truy cập bằng Get
    public record InventoryItemDto(Guid CatalogItemId, string Name , String Description , [Range(1,10000)]int Quantity, DateTimeOffset AcquiredDate);
    // Lấy dữ liệu HTTP của Play.Catalog => Để xử lý bên Inventory
    public record CatalogItemDto(Guid Id, String Name , string Description);
}