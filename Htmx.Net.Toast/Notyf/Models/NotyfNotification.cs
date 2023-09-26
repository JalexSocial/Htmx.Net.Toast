using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfNotification : Notification
{
	public NotyfNotification() : base (ToastNotificationType.Success, string.Empty, 2500)
	{
	}

	public NotyfNotification(ToastNotificationType type, string message, int? duration) : base(type,
		message, duration)
	{
	}

	public NotyfNotification(string type, string message, int? duration) : base(ToastNotificationType.Custom("custom"),
		message, duration)
	{
		CustomTypeName = type;
	}

	public object? Icon { get; set; }
	public string? CustomTypeName { get; set; } = string.Empty;
}