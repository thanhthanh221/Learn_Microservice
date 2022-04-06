using System.ComponentModel.DataAnnotations;

namespace play.catalog.service.Dtos
{
    public record ItemDto(Guid Id,string Name, String Description, decimal Price, DateTimeOffset CreateAtt);

    public record CreateItemDto([Required]string Name , string Description,[Range(0,1000)] decimal Price);

    public record UpdateItemDto([Required]string Name, string Description,[Range(0,1000)] decimal Price);
}