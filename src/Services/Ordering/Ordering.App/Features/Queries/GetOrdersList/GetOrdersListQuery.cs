using MediatR;

namespace Ordering.App.Features.Queries.GetOrdersList
{
	public class GetOrdersListQuery : IRequest<List<OrdersVm>>
	{
		public string UserName { get; set; }

		public GetOrdersListQuery(string userName)
		{
			UserName = userName;
		}
	}
}
