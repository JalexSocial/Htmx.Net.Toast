using System.Collections.Generic;
using Htmx.Net.Toast.Notyf.Enums;
using System.Text.Json.Serialization;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfConfigOptions
{
	public int? Duration { get; set; }
	public NotyfPosition Position { get; set; } = NotyfPosition.BottomRight;
	public bool? IsDismissable { get; set; } = false;
	public bool? HasRippleEffect { get; set; } = true;
	public List<NotyfNotificationOptions> CustomTypes { get; set; } = new();
}