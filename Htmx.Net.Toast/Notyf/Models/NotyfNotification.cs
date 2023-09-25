﻿using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Enums;

namespace Htmx.Net.Toast.Notyf.Models;

public class NotyfNotification : Notification
{
	public NotyfNotification() : base (ToastNotificationType.Success, string.Empty, 1)
	{
	}

	public NotyfNotification(ToastNotificationType type, string message, int? durationInSeconds) : base(type,
		message, durationInSeconds)
	{
	}

	public string Icon { get; set; }
}