﻿using Ordering.Domain.Entities;

namespace Ordering.App.Contracts.Persistence
{
	public interface IOrderRepository : IAsyncRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
	}
}
