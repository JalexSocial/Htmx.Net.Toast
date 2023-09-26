using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Helpers;
using Htmx.Net.Toast.Notyf;
using Htmx.Net.Toast.Notyf.Models;
using Microsoft.AspNetCore.Mvc;

namespace Htmx.Net.Toast.Views.Shared.Components.Notyf;

[ViewComponent(Name = "Notyf")]
public class NotyfViewComponent : ViewComponent
{
	private readonly INotyfService _service;

	public NotyfViewComponent(INotyfService service, NotyfConfig options)
	{
		_service = service;
		_options = options;
	}

	public NotyfConfig _options { get; }

	public IViewComponentResult Invoke()
	{
		var model = new NotyfViewModel
		{
			Configuration = _options.ToJson(),
			Notifications = _service.ReadAllNotifications()
		};
		return View("Default", model);
	}
}