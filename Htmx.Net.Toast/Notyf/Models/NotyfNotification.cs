using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfNotification : Notification
{
	public NotyfNotification() : base(ToastNotificationType.Success, string.Empty, 2500)
	{
	}

	public NotyfNotification(ToastNotificationType type, string message, int? duration, object? icon = null) : base(type,
		message, duration)
	{
		Icon = icon;
	}

	public object? Icon { get; set; }
}