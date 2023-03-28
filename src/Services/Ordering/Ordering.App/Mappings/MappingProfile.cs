using AutoMapper;
using Ordering.App.Features.Commands.CheckOutOrder;
using Ordering.App.Features.Commands.UpdateOrder;
using Ordering.App.Features.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.App.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrdersVm>().ReverseMap();
			CreateMap<Order, CheckOutOrderCommand>().ReverseMap();
			CreateMap<Order, UpdateOrderCommand>().ReverseMap();

		}
	}
}
