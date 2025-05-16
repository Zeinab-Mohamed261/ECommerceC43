using Microsoft.AspNetCore.Mvc;
using ServicesAbstrations;
using Shared.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contollers
{
    [ApiController]
    [Route("api/[controller]")]  //   baseUrl/api/Basket
    public class BasketController(IServiceManager _serviceManager) :ControllerBase
    {
        //Get Basket
        [HttpGet] //GET   BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }
        //Create or Update Basket
        [HttpPost]  //Post     BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        //Delete Basket
        [HttpDelete("{Key}")] //DELETE   BaseUrl/api/Basket
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
