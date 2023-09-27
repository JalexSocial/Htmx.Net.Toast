using System;
using System.Text.Json.Serialization;
using Htmx.Net.Toast.Enums;
using Htmx.Net.Toast.Helpers;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfNotificationOptions
{
	private object _icon = false;

	[JsonPropertyName("type")]
	[JsonConverter(typeof(ToastNotificationTypeConverter))]
	public ToastNotificationType? Type { get; set; }
	[JsonPropertyName("className")]
	public string? ClassName { get; set; }
	[JsonPropertyName("duration")]
	public int? Duration { get; set; }
	[JsonPropertyName("icon")]
	public object Icon
	{
		get => _icon;
		set
		{
			if (value is NotyfIcon or string or bool)
				_icon = value;
			else
				throw new Exception("Icon must be either NotyfIcon, string, or bool");
		}
	} 
	[JsonPropertyName("background")]
	public string? BackgroundColor { get; set; }
	[JsonPropertyName("message")]
	public string? Message { get; set; }
	[JsonPropertyName("ripple")]
	public bool? Ripple { get; set; }
	[JsonPropertyName("dismissable")]
	public bool? Dismissible { get; set; }

}