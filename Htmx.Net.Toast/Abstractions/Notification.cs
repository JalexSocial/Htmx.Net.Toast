using System.Text.Json.Serialization;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Abstractions;

public class Notification
{
	public Notification(ToastNotificationType type, string message, int? duration)
	{
		Message = message;
		Type = type;
		Duration = duration == null || duration == 0 ? null : duration;
	}

	[JsonPropertyName("message")]
	public string Message { get; set; }
	[JsonPropertyName("backgroundColor")]
	public string? BackgroundColor { get; set; }
	[JsonPropertyName("type")]
	public ToastNotificationType Type { get; set; }
	[JsonPropertyName("duration")]
	public int? Duration { get; set; }
}