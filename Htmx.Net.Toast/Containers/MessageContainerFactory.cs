using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Helpers;
using Microsoft.AspNetCore.Http;

namespace Htmx.Net.Toast.Containers;

internal class MessageContainerFactory : IMessageContainerFactory
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly ITempDataService _tempDataWrapper;

	public MessageContainerFactory(IHttpContextAccessor httpContextAccessor, ITempDataService tempDataWrapper)
	{
		_httpContextAccessor = httpContextAccessor;
		_tempDataWrapper = tempDataWrapper;
	}

	public IToastNotificationContainer<TMessage> Create<TMessage>()
		where TMessage : class
	{
		var httpContext = _httpContextAccessor.HttpContext;
		if (httpContext != null && httpContext.Request.IsHtmx())
			return new InMemoryNotificationContainer<TMessage>();
		return new TempDataToastNotificationContainer<TMessage>(_tempDataWrapper);
	}
}