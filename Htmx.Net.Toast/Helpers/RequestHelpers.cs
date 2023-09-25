using Microsoft.AspNetCore.Http;
using System;

namespace Htmx.Net.Toast.Helpers;

internal static class RequestHelpers
{
	public static bool IsHtmx(this HttpRequest request)
	{
		if (request == null) throw new ArgumentNullException(nameof(request));

		if (request?.Headers.ContainsKey(Constants.RequestHeaderKey) is true) return true;

		return false;
	}
}