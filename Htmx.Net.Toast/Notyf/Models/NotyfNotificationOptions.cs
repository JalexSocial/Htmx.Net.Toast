using System;
using System.Text.Json.Serialization;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfNotificationOptions
{
	private object _icon = false;

	[JsonPropertyName("type")]
	public string? NotificationType { get; set; }
	[JsonPropertyName("className")]
	public string? ClassName { get; set; }
	[JsonPropertyName("duration")]
	public int? Duration { get; set; }
	[JsonPropertyName("icon")]
	public object Icon
	{
		get { return _icon;  }
		set
		{
			if (value is NotyfIcon || value is string || value is bool)
				_icon = value;
			else
				throw new Exception("Icon must be either NotyfIcon, string, or bool");
		}
	} 
	[JsonPropertyName("background")]
	public string? Background { get; set; }
	[JsonPropertyName("message")]
	public string? Message { get; set; }
	[JsonPropertyName("ripple")]
	public bool? Ripple { get; set; }
	[JsonPropertyName("dismissable")]
	public bool? Dismissible { get; set; }

}