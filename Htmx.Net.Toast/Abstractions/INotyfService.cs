using Htmx.Net.Toast.Enums;
using Htmx.Net.Toast.Notyf.Models;
using System.Collections.Generic;

namespace Htmx.Net.Toast.Abstractions;

public interface INotyfService
{
	void Success(string message, int? duration = null);
	void Error(string message, int? duration = null);
	void Information(string message, int? duration = null);
	void Warning(string message, int? duration = null);
	void Custom(ToastNotificationType type, string message, int? duration = null, object? icon = null);

	IEnumerable<NotyfNotification> GetNotifications();
	IEnumerable<NotyfNotification> ReadAllNotifications();
	void RemoveAll();
}