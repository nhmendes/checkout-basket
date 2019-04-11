namespace BasketService.Application.Services.TypeAdapters
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using BasketService.Application.DTO.Baskets;

    [ExcludeFromCodeCoverage]
    public class BasketsTypeAdapter : IBasketsTypeAdapter
    {
        public Basket ToDto(Domain.Model.Baskets.Basket from)
        {
            if (null == from)
            {
                return null;
            }

            return new Basket
            {
                Id = from.Id,
                CustomerEmail = from.CustomerEmail,
                Items = ToDto(from.Items)
            };
        }

        public IEnumerable<Basket> ToDto(IEnumerable<Domain.Model.Baskets.Basket> from)
        {
            if (null == from)
            {
                yield return null;
            }

            foreach (var basket in from)
            {
                yield return ToDto(basket);
            }
        }

        public IEnumerable<Item> ToDto(IEnumerable<Domain.Model.Baskets.Item> items)
        {
            if (null == items)
            {
                yield return null;
            }

            foreach (var item in items)
            {
                yield return this.ToDto(item);
            }
        }

        public IEnumerable<Domain.Model.Baskets.Item> ToDomain(IEnumerable<Item> items)
        {
            if (null == items)
            {
                yield return null;
            }

            foreach (var item in items)
            {
                yield return this.ToDomain(item);
            }
        }

        public Domain.Model.Baskets.Item ToDomain(Item from)
        {
            if (null == from)
            {
                return null;
            }

            return Domain.Model.Baskets.Item.Create(
                itemId: from.ItemId, 
                variant: from.Variant, 
                quantity: from.Quantity);
        }

        public DTO.Baskets.Item ToDto(Domain.Model.Baskets.Item from)
        {
            if (null == from)
            {
                return null;
            }

            return new Item
            {
                ItemId = from.ItemId,
                Variant = from.Variant,
                Quantity = from.Quantity
            };
        }
    }
}