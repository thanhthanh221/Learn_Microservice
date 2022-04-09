using MassTransit;
using Play.Inventory.Service.Entities;
using Play.Common;
using Play.Catalog.Contracts;
namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem>? repository ;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem>? repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            CatalogItemUpdated? message = context.Message;

            CatalogItem? item =  await repository.GetAsync(message.ItemId); 

            if(item == null)
            {
                item = new CatalogItem
                {
                    Id = message.ItemId,
                    Name = message.Name,
                    Description = message.Description
                };

                await repository.CreateAsync(item);
            }
            else
            {
                item.Name = message.Name;
                item.Description = message.Description;

                await repository.UpdateAsync(item);
            }
            
        }
    }
}
