namespace BasketService.Presentation.WebApi.Controllers
{
    using System;
    using System.Threading.Tasks;
    using BasketService.Application.DTO.Baskets;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Infrastructure.CrossCutting.Exceptions;
    using BasketService.Infrastructure.CrossCutting.Logging;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class BasketItemsController : ControllerBase
    {
        private readonly ILog logger;
        private readonly IInsertBasketItem insertBasketItem;
        private readonly IGetBasketItemById getBasketItemById;
        private readonly IUpdateBasketItem updateBasketItem;
        private readonly IDeleteBasketItem deleteBasketItem;

        /// <summary>
        /// Initializes a new BasketService.Presentation.WebApi.Controllers.BasketItemsController
        /// </summary>
        /// <param name="insertBasketItem"></param>
        /// <param name="updateBasketItem"></param>
        /// <param name="deleteBasketItem"></param>
        public BasketItemsController(
            ILog logger,
            IGetBasketItemById getBasketItemById,
            IInsertBasketItem insertBasketItem,
            IUpdateBasketItem updateBasketItem,
            IDeleteBasketItem deleteBasketItem
            )
        {
            this.logger = logger;
            this.insertBasketItem = insertBasketItem;
            this.getBasketItemById = getBasketItemById;
            this.updateBasketItem = updateBasketItem;
            this.deleteBasketItem = deleteBasketItem;
        }

        [HttpDelete]
        [Route("{basketId}/items/{itemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBasketItemById(string basketId, string itemId)
        {
            try
            {
                if (string.IsNullOrEmpty(basketId) || string.IsNullOrEmpty(itemId))
                {
                    return this.BadRequest();
                }

                await this.deleteBasketItem
                    .Execute(DeleteBasketItemRequest.Create(basketId, itemId));

                return NoContent();
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{basketId}/items")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddItemToBasket(string basketId, string itemVariant, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(basketId) || string.IsNullOrEmpty(itemVariant) || quantity <= 0)
                {
                    return BadRequest();
                }

                var result = await this.insertBasketItem
                    .Execute(InsertBasketItemRequest.Create(basketId, itemVariant, quantity));

                return this.CreatedAtRoute(
                    routeName: nameof(GetBasketItemById),
                    routeValues:
                        new
                        {
                            basketId = basketId,
                            itemId = result.ItemId
                        },
                    value: result);
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{basketId}/items/{itemId}", Name = nameof(GetBasketItemById))]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBasketItemById(string basketId, string itemId)
        {
            try
            {
                if (string.IsNullOrEmpty(basketId) || string.IsNullOrEmpty(itemId))
                {
                    return BadRequest();
                }

                var result = await this.getBasketItemById
                    .Execute(GetBasketItemByIdRequest.Create(basketId, itemId));

                if (null == result)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("{basketId}/items/{itemId}")]
        [ProducesResponseType(typeof(Item), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PatchBasketItem(string basketId, string itemId, [FromBody]JsonPatchDocument<Item> jsonPatchDocument)
        {
            try
            {
                var basketItem = await this.getBasketItemById
                    .Execute(GetBasketItemByIdRequest.Create(basketId, itemId));
                if (null == basketItem)
                {
                    return NotFound();
                }

                jsonPatchDocument.ApplyTo(basketItem);

                await this.updateBasketItem
                    .Execute(UpdateBasketItemRequest.Create(basketId, basketItem));

                return new ObjectResult(basketItem);
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message);
                return BadRequest();
            }
        }
    }
}