using System;
using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Demo.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly INotyfService _notyf;
	public HomeController(ILogger<HomeController> logger, INotyfService notyf)
	{
		_logger = logger;
		_notyf = notyf;
	}

	public IActionResult Index()
	{
		//_notyf.Success("Success Notification");
		_notyf.Success("Success Notification that closes in 10 Seconds.", 10000);
		_notyf.Error("Some Error Message");
		_notyf.Warning("Some Error Message");
		_notyf.Information("Information Notification - closes in 4 seconds.", 4000);
		//_notyf.Custom("Custom Notification <br><b><i>closes in 5 seconds.</i></b></p>", 5, "indigo", "fa fa-gear");
		_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 5 seconds.", 5000);
		_notyf.Custom(ToastNotificationType.Custom("rawr"), "Custom Notification - closes in 10 seconds.", 10000);
		return View();
	}

	public IActionResult Notifications()
	{
		_notyf.Success("Success Notification invoked via htmx");
		_notyf.Error("Some Error Message 1");
		_notyf.Information("Information Notification - closes in 4 seconds.", 45000);
		_notyf.Warning("Some Error Message");

		Random rand = new Random(System.Environment.TickCount);
		var num = rand.NextInt64(1, 5000);

		return Content($"{num} - This content was generated via an htmx call.");
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
