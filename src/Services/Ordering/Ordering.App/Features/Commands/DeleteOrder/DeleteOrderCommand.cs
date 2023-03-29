using MediatR;

namespace Ordering.App.Features.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest
	{
		public int Id { get; set; }
	}
}
