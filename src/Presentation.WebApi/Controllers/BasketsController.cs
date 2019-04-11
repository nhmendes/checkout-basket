namespace BasketService.Presentation.WebApi.Controllers
{
    using System.Threading.Tasks;
    using BasketService.Application.Services.UseCases.Interfaces;
    using BasketService.Application.Services.UseCases.RequestModel;
    using BasketService.Infrastructure.CrossCutting.Validation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [Authorize]
    public class BasketsController : ControllerBase
    {
        private const string basketIdTemplate = "{basketId}";
        private const string GetBasketsByIdUriTemplate = basketIdTemplate;
        private const string DeleteBasketUriTemplate = basketIdTemplate;
        
        private readonly IGetAllBaskets getAllBaskets;
        private readonly IGetBasketsById getBasketsById;
        private readonly IInsertBasket insertBasket;
        private readonly IDeleteBasket deleteBasket;

        /// <summary>
        /// Initializes a new BasketService.Presentation.WebApi.Controllers.BasketsController
        /// </summary>
        /// <param name="getAllBaskets">The usecase for getting all customers baskets</param>
        /// <param name="getBasketsById">The usecase for getting a customer basket by id</param>
        /// <param name="insertBasket">The usecase for creating a new basket</param>
        /// <param name="deleteBasket">The usecase for deleting a basket by id</param>
        public BasketsController(
            IGetAllBaskets getAllBaskets,
            IGetBasketsById getBasketsById,
            IInsertBasket insertBasket,
            IDeleteBasket deleteBasket
            )
        {
            this.getAllBaskets = getAllBaskets;
            this.getBasketsById = getBasketsById;
            this.insertBasket = insertBasket;
            this.deleteBasket = deleteBasket;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBaskets([FromHeader(Name = "customer_email")] string customerEmail)
        {
            var result = await this.getAllBaskets
                .Execute(GetAllBasketsRequest.Create());

            if (null == result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route(GetBasketsByIdUriTemplate, Name = nameof(GetBaskets))]
        public async Task<IActionResult> GetBaskets(string basketId)
        {
            var result = await this.getBasketsById
                .Execute(GetBasketsByIdRequest.Create(basketId));

            if (null == result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertBasket(string customerEmail)
        {
            if (string.IsNullOrEmpty(customerEmail))
            {
                return this.BadRequest();
            }

            var result = await this.insertBasket
                .Execute(InsertBasketRequest.Create(customerEmail));

            return this.CreatedAtRoute(
                routeName: nameof(GetBaskets),
                routeValues: new { basketId = result.Id },
                value: result);
        }

        [HttpDelete]
        [Route(DeleteBasketUriTemplate)]
        public async Task<IActionResult> DeleteBasket(string basketId)
        {
            if (string.IsNullOrEmpty(basketId))
            {
                return this.BadRequest();
            }

            await this.deleteBasket
                .Execute(DeleteBasketRequest.Create(basketId));

            return Ok();
        }
    }
}