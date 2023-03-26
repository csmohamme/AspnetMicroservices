using AutoMapper;
using Ordering.App.Features.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.App.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Order, OrdersVm>().ReverseMap();

		}
	}
}
