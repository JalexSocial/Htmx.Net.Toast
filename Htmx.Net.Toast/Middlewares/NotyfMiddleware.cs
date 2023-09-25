using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Helpers;
using Htmx.Net.Toast.Notyf;
using Htmx.Net.Toast.Notyf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Htmx.Net.Toast.Middlewares;

internal class NotyfMiddleware
	: IMiddleware
{
	private const string AccessControlExposeHeadersKey = "Access-Control-Expose-Headers";
	private readonly ILogger<NotyfMiddleware> _logger;
	private readonly INotyfService _toastNotification;

	public NotyfMiddleware(INotyfService toastNotification, ILogger<NotyfMiddleware> logger, NotyfEntity options)
	{
		_toastNotification = toastNotification;
		_logger = logger;
		_options = options;
	}

	public NotyfEntity _options { get; }

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		context.Response.OnStarting(Callback, context);
		await next(context);
	}

	private Task Callback(object context)
	{
		var httpContext = (HttpContext)context;
		if (httpContext.Request.IsHtmx())
		{
			var messages = new NotyfViewModel
			{
				Configuration = _options.ToJson(),
				Notifications = _toastNotification.ReadAllNotifications()
			};
			if (messages.Notifications != null && messages.Notifications.Any())
			{
				var accessControlExposeHeaders = $"{GetControlExposeHeaders(httpContext.Response.Headers)}";
				httpContext.Response.Headers.Add(AccessControlExposeHeadersKey, accessControlExposeHeaders);

				var notificationsJson = new { notyfpublish = messages.Notifications }.ToJson();
				httpContext.Response.Headers.Add(Constants.NotyfResponseHeaderKey,
					notificationsJson);
			}
		}

		return Task.FromResult(0);
	}

	private object GetControlExposeHeaders(IHeaderDictionary headers)
	{
		var existingHeaders = headers[AccessControlExposeHeadersKey];
		if (string.IsNullOrEmpty(existingHeaders))
			return Constants.NotyfResponseHeaderKey;
		return $"{existingHeaders}, {Constants.NotyfResponseHeaderKey}";
	}
}