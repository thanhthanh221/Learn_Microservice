using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Dtos;
using System.Linq;
using play.catalog.service.Entities;
using play.Common;

namespace play.catalog.service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController: ControllerBase
    {
        private readonly IRepository<Item>? itemsRepository;
        private static int requestCounter = 0; // Số lần request trả về
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures Potion", 15, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amont of damege", 24, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 54, DateTimeOffset.UtcNow)
        };

        public ItemsController(IRepository<Item>? itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllAsync()
        {
            // Mỗi lần truy vấn đến hàm get sẽ tăng thêm 1;
            requestCounter++;

            Console.WriteLine($"So Lan Client gui {requestCounter}");

            if(requestCounter <= 2)
            {
                Console.WriteLine($"Truy van thu {requestCounter}: Delay.....");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
            if(requestCounter <= 4)
            {
                Console.WriteLine($"Truy van thu {requestCounter}: bi loi 500 (Internal Sever Error)");
                return StatusCode(500);
            }
            IReadOnlyCollection<ItemDto> items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto()).ToList();

            Console.WriteLine($"truy van thu {requestCounter}: 200 (Ok)");
            return Ok(items);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid Id)
        {
            ItemDto? itemDto = (await itemsRepository.GetAsync(Id)).AsDto();
            if(itemDto == null)
            {
                return NotFound();
            }
            return itemDto;
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItem)
        {
            Item? item = new Item(){
                Id = Guid.NewGuid(),
                Name =createItem.Name,
                Price = createItem.Price,
                Description = createItem.Description
            };
            await itemsRepository.CreateAsync(item);

            

            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);

        }
        // PUT/items/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(Guid Id, UpdateItemDto updateItemDto)
        {
            Item? item = (await itemsRepository.GetAsync(Id));
            if(item == null)
            {
                return NotFound();
            }

            item.Price = updateItemDto.Price;
            item.Name = updateItemDto.Name;
            item.Description = updateItemDto.Description;
            
            await itemsRepository.UpdateAsync(item);
            return NoContent();

        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ItemDto>> Delete(Guid Id)
        {
            Item item = await itemsRepository.GetAsync(Id);
            
            if(item == null)
            {
                return NotFound();
            }
            await itemsRepository.DeleteAsync(Id);
            return NoContent();
            
        }


    }
}