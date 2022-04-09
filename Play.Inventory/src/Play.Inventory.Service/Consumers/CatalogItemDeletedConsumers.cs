using MassTransit;
using Play.Inventory.Service.Entities;
using Play.Common;
using Play.Catalog.Contracts;
namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemDeleteConsumer : IConsumer<CatalogItemDelete>
    {
        private readonly IRepository<CatalogItem>? repository ;

        public CatalogItemDeleteConsumer(IRepository<CatalogItem>? repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemDelete> context)
        {
            CatalogItemDelete? message = context.Message;

            CatalogItem? item =  await repository.GetAsync(message.ItemId); 

            if(item == null)
            {
                return;
            }
            await repository.DeleteAsync(item.Id);
            
        }
    }
}
