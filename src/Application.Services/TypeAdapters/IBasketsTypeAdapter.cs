namespace BasketService.Application.Services.TypeAdapters
{
    using System.Collections.Generic;
    using BasketService.Application.DTO.Baskets;

    public interface IBasketsTypeAdapter
    {
        Basket ToDto(Domain.Model.Baskets.Basket from);

        IEnumerable<Basket> ToDto(IEnumerable<Domain.Model.Baskets.Basket> from);

        IEnumerable<Item> ToDto(IEnumerable<Domain.Model.Baskets.Item> items);

        IEnumerable<Domain.Model.Baskets.Item> ToDomain(IEnumerable<Item> items);

        Domain.Model.Baskets.Item ToDomain(Item item);

        Item ToDto(Domain.Model.Baskets.Item domainItem);
    }
}