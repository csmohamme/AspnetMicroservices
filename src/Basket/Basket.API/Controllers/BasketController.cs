﻿using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _repository;
		private readonly DiscountGrpcService _discountGrpc;

		public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpc)
		{
			_repository = repository;
			_discountGrpc = discountGrpc;
		}

		[HttpGet("{userName}", Name = "GetBasket")]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
		{
			var basket = await _repository.GetBasket(userName);
			return Ok(basket ?? new ShoppingCart(userName));
		}

		[HttpPost]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
		{
			// Todo: Communicate with Discount.Grpc and calculate latest price
			foreach (var item in basket.Items)
			{
				var coupon = await _discountGrpc.GetDiscount(item.ProductName);
				item.Price -= coupon.Amount;
			}
			return Ok(await _repository.UpdateBasket(basket));
		}

		[HttpDelete("{userName}", Name = "DeleteBasket")]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeleteBasket(string userName)
		{
			await _repository.DeleteBasket(userName);
			return Ok();
		}
	}
}
