using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Containers;
using Htmx.Net.Toast.Middlewares;
using Htmx.Net.Toast.Notyf;
using Htmx.Net.Toast.Notyf.Models;
using Htmx.Net.Toast.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Htmx.Net.Toast.Extensions;

public static class ServiceCollectionExtensions
{
	private static void AddFrameworkServices(this IServiceCollection services)
	{
		#region Framework Services

		//Check if a TempDataProvider is already registered.
		var tempDataProvider = services.FirstOrDefault(d => d.ServiceType == typeof(ITempDataProvider));
		if (tempDataProvider == null)
			//Add a tempdata provider when one is not already registered
			services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
		//check if IHttpContextAccessor implementation is not registered. Add one if not.
		var httpContextAccessor = services.FirstOrDefault(d => d.ServiceType == typeof(IHttpContextAccessor));
		if (httpContextAccessor == null) services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		#endregion
	}

	public static void AddNotyf(this IServiceCollection services, Action<NotyfConfigOptions> configure)
	{
		var configurationValue = new NotyfConfigOptions();
		configure(configurationValue);
		var options = new NotyfConfig(configurationValue.Position, configurationValue.CustomTypes)
		{
			Duration = configurationValue.Duration ?? 5000,
			Dismissible = configurationValue.IsDismissable ?? false,
			Ripple = configurationValue.HasRippleEffect ?? true
		};
		if (services == null) throw new ArgumentNullException(nameof(services));

		services.AddFrameworkServices();

		//Add TempDataWrapper for accessing and adding values to tempdata.
		services.AddSingleton<ITempDataService, TempDataService>();
		// Add MessageContainerFactory for creating MessageContainer instance
		services.AddSingleton<IMessageContainerFactory, MessageContainerFactory>();

		//Add the ToastNotification implementation
		services.AddScoped<INotyfService, NotyfService>();
		//Middleware
		services.AddScoped<NotyfMiddleware>();
		services.AddSingleton(options);
	}
}