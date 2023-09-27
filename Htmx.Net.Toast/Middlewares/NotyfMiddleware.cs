using System.Collections.Generic;
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
using System.Reflection.PortableExecutable;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;

namespace Htmx.Net.Toast.Middlewares;

internal class NotyfMiddleware
	: IMiddleware
{
	private const string AccessControlExposeHeadersKey = "Access-Control-Expose-Headers";
	private readonly ILogger<NotyfMiddleware> _logger;
	private readonly INotyfService _toastNotification;

	public NotyfMiddleware(INotyfService toastNotification, ILogger<NotyfMiddleware> logger, NotyfConfig options)
	{
		_toastNotification = toastNotification;
		_logger = logger;
		_options = options;
	}

	public NotyfConfig _options { get; }

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
			if (messages.Notifications.Any())
			{
				var accessControlExposeHeaders = $"{GetControlExposeHeaders(httpContext.Response.Headers)}";
				httpContext.Response.Headers.Add(AccessControlExposeHeadersKey, accessControlExposeHeaders);

				var triggers = new Dictionary<string, object?>();

				// Code partially written by Khalid Abuhakmeh
				if (httpContext.Response.Headers.ContainsKey(HtmxHeaderKeys.ResponseTriggerAfterSettle))
				{
					var header = httpContext.Response.Headers[HtmxHeaderKeys.ResponseTriggerAfterSettle];

					// Attempt to parse existing header as Json, if fails it is a simplified event key
					// assume if the string starts with '{' and ends with '}', that it is JSON
					if (header.Any(h => h is ['{', .., '}']))
					{
						var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(header));
						// this might still throw :(
						var jsonObject = JsonNode.Parse(ref reader)?.AsObject();
						// Load any existing triggers
						foreach (var (key, value) in jsonObject!)
							triggers.Add(key, value);
					}
					else
					{
						foreach (var headerValue in header)
						{
							if (headerValue is null) continue;

							var eventNames = headerValue.Split(',');

							foreach (var eventName in eventNames)
								triggers.Add(eventName, null);
						}
					}
				}

				triggers.TryAdd(HtmxHeaderKeys.ResponseTriggerPublishNotificationKey, messages.Notifications);

				var notificationsJson = triggers.ToJson();

				httpContext.Response.Headers[HtmxHeaderKeys.ResponseTriggerAfterSettle] = notificationsJson;
			}
		}

		return Task.FromResult(0);
	}
	/*
	private void ParsePossibleExistingTriggers(string headerKey, HtmxTriggerTiming timing)
	{
		if (!_headers.ContainsKey(headerKey))
			return;

		var header = _headers[headerKey];
		// Attempt to parse existing header as Json, if fails it is a simplified event key
		// assume if the string starts with '{' and ends with '}', that it is JSON
		if (header.Any(h => h is ['{', .., '}']))
		{
			var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(header));
			// this might still throw :(
			var jsonObject = JsonNode.Parse(ref reader)?.AsObject();
			// Load any existing triggers
			foreach (var (key, value) in jsonObject!)
				WithTrigger(key, value, timing);
		}
		else
		{
			foreach (var headerValue in _headers[headerKey])
			{
				if (headerValue is null) continue;

				var eventNames = headerValue.Split(',');

				foreach (var eventName in eventNames)
					WithTrigger(eventName, null, timing);
			}
		}
	}
	*/
	private object GetControlExposeHeaders(IHeaderDictionary headers)
	{
		var existingHeaders = headers[AccessControlExposeHeadersKey];
		if (string.IsNullOrEmpty(existingHeaders))
			return HtmxHeaderKeys.ResponseTriggerAfterSettle;
		return $"{existingHeaders}, {HtmxHeaderKeys.ResponseTriggerAfterSettle}";
	}
}