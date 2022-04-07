using System;
namespace Play.Catalog.Contracts
{
    public record CatalogItemCreacted(Guid ItemId, String Name , string Description);
    public record CatalogItemUpdated(Guid ItemId, String Name , string Description);
    public record CatalogItemDelete(Guid ItemId);

}
