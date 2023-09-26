using Microsoft.AspNetCore.Http;
using System;

namespace Htmx.Net.Toast.Helpers;

internal static class RequestHelpers
{
	public static bool IsHtmx(this HttpRequest request)
	{
		if (request == null) throw new ArgumentNullException(nameof(request));

		if (request?.Headers.ContainsKey(HtmxHeaderKeys.Request) is true) return true;

		return false;
	}
}