using MassTransit;
using Play.Inventory.Service.Entities;
using Play.Common;
using Play.Catalog.Contracts;
namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemCreactedConsumer : IConsumer<CatalogItemCreacted>
    {
        private readonly IRepository<CatalogItem>? repository ;

        public CatalogItemCreactedConsumer(IRepository<CatalogItem>? repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemCreacted> context)
        {
            CatalogItemCreacted? message = context.Message;

            CatalogItem? item =  await repository.GetAsync(message.ItemId); 

            if(item != null)
            {
                return ;
            }
            item = new CatalogItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };

            await repository.CreateAsync(item);
            
        }
    }
}
