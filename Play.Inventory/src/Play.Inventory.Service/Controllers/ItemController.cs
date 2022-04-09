using Microsoft.AspNetCore.Mvc;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Clients;
using Play.Common;

namespace Play.Inventory.Service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem>? inventoryItemRepository;
        private readonly IRepository<CatalogItem>?  catalogItemRepository;

        public ItemsController(IRepository<InventoryItem>? inventoryItemRepository, IRepository<CatalogItem>? catalogItemRepository)
        {
            this.inventoryItemRepository = inventoryItemRepository;
            this.catalogItemRepository = catalogItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest(); // Trả về mã  400 không tìm thấy dữ liệu
            }

            IEnumerable<InventoryItem> inventoryItemsEntities = await inventoryItemRepository.GetAllAsync(item => item.UserId.Equals(userId));

            IEnumerable<Guid> itemIds = inventoryItemsEntities.Select(p => p.CatalogItemId);

            IEnumerable<CatalogItem> catalogItems = await catalogItemRepository.GetAllAsync(p => itemIds.Contains(p.Id));


            var inventoryItemDto = inventoryItemsEntities.Select(inventoryItem => 
            {
                // Lấy ra cái sản phẩm mà có Id bằng với id nhập vào
                CatalogItem catalogItem = catalogItems.Single(catalogitem => catalogitem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);
            });

            return Ok(inventoryItemDto); // Trả về mã 200 thỏa mãn
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemDto grantItemDto)
        {
            // Tìm sản phẩm mà người tạo là người dùng đó và  có danh mục sản phẩm ấy
            InventoryItem inventoryItem = await inventoryItemRepository
            .
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
                await inventoryItemRepository.CreateAsync(inventoryItem);
            }
            else
            {
                // Nếu đã có rồi thì số lượng sẽ được cộng thêm
                inventoryItem.Quatity += grantItemDto.Quantity;
                // Update vào trong csdl
                await inventoryItemRepository.UpdateAsync(inventoryItem);
            }
            
            return Ok(inventoryItem);
        }
    }
}