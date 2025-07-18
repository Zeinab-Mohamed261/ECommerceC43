﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManager serviceManager) :ControllerBase
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var Basket =await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
        [HttpPost("WebHook")]
        public async Task<ActionResult> WebHook()
        {
            var json =await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            //Logic
            await serviceManager.PaymentService.UpdateOrderPaymentStatus(json, Request.Headers["Stripe-Signature"]);


            return new EmptyResult();
        }

    }


}
