
using Ordering.App.Models;

namespace Ordering.App.Contracts.Infrastructure
{
	public interface IEmailService
	{
		Task<bool> SendEmail(Email email);
	}
}
