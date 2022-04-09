using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using MassTransit;
using Play.Catalog.Contracts;
using Play.Common;
using play.catalog.service.Entities;
using play.catalog.service;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController: ControllerBase
    {
        private readonly IRepository<Item>? itemsRepository ;
        private readonly IPublishEndpoint publishEndpoint ;
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures Potion", 15, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amont of damege", 24, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 54, DateTimeOffset.UtcNow)
        };

        public ItemsController(IRepository<Item>? itemsRepository, IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllAsync()
        {

            IReadOnlyCollection<ItemDto> items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto()).ToList();

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

            await publishEndpoint.Publish(new CatalogItemCreacted(item.Id, item.Name, item.Description));

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
            
            await publishEndpoint.Publish(new CatalogItemUpdated(item.Id, item.Name, item.Description));

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

            await publishEndpoint.Publish(new CatalogItemDelete(item.Id));
            return NoContent();
            
        }


    }
}