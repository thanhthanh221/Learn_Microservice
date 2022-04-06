using Microsoft.AspNetCore.Mvc;
using play.Common;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Clients;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem>? itemsRepository;
        private readonly CataLogClient? cataLogClient;

        public ItemsController(IRepository<InventoryItem>? itemsRepository, CataLogClient? cataLogClient)
        {
            this.itemsRepository = itemsRepository;
            this.cataLogClient = cataLogClient;
        }
        [HttpGet]
        public async Task<ActionResult<List<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest(); // Trả về mã  400 không tìm thấy dữ liệu
            }
            // Tất cả các sản phẩm bên service Play.catalog
            var catalogItems = await cataLogClient.GetCatalogItemsAsync();

            var items = await itemsRepository.GetAllAsync(c => c.UserId == userId);

            var inventoryItemDto = items.Select(inventoryItem => 
            {
                // Lấy ra cái sản phẩm mà có  Id bằng với id nhập vào
                var catalogItem = catalogItems.Single(catalogitem => catalogitem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);
            });

            return Ok(inventoryItemDto); // Trả về mã 200 thỏa mãn
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemDto grantItemDto)
        {
            // Tìm sản phẩm mà người tạo là người dùng đó và  có danh mục sản phẩm ấy
            InventoryItem inventoryItem = await itemsRepository.
                GetAsync(c => c.UserId == grantItemDto.UserId && c.CatalogItemId == grantItemDto.CatalogItemId);

            if(inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemDto.CatalogItemId,
                    UserId = grantItemDto.UserId,
                    Quatity = grantItemDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };
                // Đẩy vào MongoDb
                await itemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                // Nếu đã có rồi thì số lượng sẽ được cộng thêm
                inventoryItem.Quatity += grantItemDto.Quantity;
                // Update vào trong csdl
                await itemsRepository.UpdateAsync(inventoryItem);
            }
            
            return Ok(inventoryItem);
        }
    }
}