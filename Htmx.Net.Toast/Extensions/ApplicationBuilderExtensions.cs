using Htmx.Net.Toast.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Htmx.Net.Toast.Extensions;

public static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseNotyf(this IApplicationBuilder builder)
	{
		builder.UseMiddleware<NotyfMiddleware>();
		return builder;
	}
}