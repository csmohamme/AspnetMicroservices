using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.App.Contracts.Persistence;
using Ordering.App.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.App.Features.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<DeleteOrderCommandHandler> _logger;

		public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
			if (orderToDelete == null)
			{
				throw new NotFoundException(nameof(Order), request.Id);
			}
			await _orderRepository.DeleteAsync(orderToDelete);
			_logger.LogInformation($"order {orderToDelete.Id} is successfully deleted.");
			return Unit.Value;
		}
	}
}
