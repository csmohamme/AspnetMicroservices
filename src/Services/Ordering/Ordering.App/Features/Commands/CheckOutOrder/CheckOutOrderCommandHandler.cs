using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.App.Contracts.Infrastructure;
using Ordering.App.Contracts.Persistence;
using Ordering.App.Models;
using Ordering.Domain.Entities;

namespace Ordering.App.Features.Commands.CheckOutOrder
{
	public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;
		private readonly IEmailService _email;
		private readonly ILogger<CheckOutOrderCommandHandler> _logger;

		public CheckOutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
			IEmailService email, ILogger<CheckOutOrderCommandHandler> logger)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
			_email = email;
			_logger = logger;
		}

		public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
		{
			var orderEntity = _mapper.Map<Order>(request);
			var newOrder = await _orderRepository.AddAsync(orderEntity);
			_logger.LogInformation($"Order {newOrder.Id} is successfully created.");
			await SendMail(newOrder);
			return newOrder.Id;
		}

		private async Task SendMail(Order order)
		{
			var email = new Email() { To = "root32392@gmail.com", Body = $"Order was created", Subject = "Order was created" };
			try
			{
				await _email.SendEmail(email);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Order {order.Id} failed due to an error with email service: {ex.Message}");
			}
		}
	}
}
