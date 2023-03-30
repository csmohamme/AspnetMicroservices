using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.App.Features.Commands.CheckOutOrder;
using Ordering.App.Features.Commands.DeleteOrder;
using Ordering.App.Features.Commands.UpdateOrder;
using Ordering.App.Features.Queries.GetOrdersList;
using System.Net;

namespace Ordering.API.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{userName}", Name = "GetOrder")]
		[ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrderByUserName(string userName)
		{
			var query = new GetOrdersListQuery(userName);
			var orders = await _mediator.Send(query);
			return Ok(orders);
		}

		//Testing purpose
		[HttpPost(Name = "CheckoutOrder")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckOutOrderCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(command);
		}

		[HttpPut(Name = "UpdateOrder")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
		{
			var result = await _mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}", Name = "DeleteOrder")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesDefaultResponseType]
		public async Task<ActionResult> DeleteOrder(int id)
		{
			var command = new DeleteOrderCommand() { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
