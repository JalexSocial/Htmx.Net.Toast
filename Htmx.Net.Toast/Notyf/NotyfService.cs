using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Containers;
using Htmx.Net.Toast.Enums;
using Htmx.Net.Toast.Notyf.Models;
using System.Collections.Generic;

namespace Htmx.Net.Toast.Notyf;

public class NotyfService : INotyfService
{
	private readonly IToastNotificationContainer<NotyfNotification> _messageContainer;

	public NotyfService(IMessageContainerFactory messageContainerFactory)
	{
		_messageContainer = messageContainerFactory.Create<NotyfNotification>();
	}

	public IEnumerable<NotyfNotification> GetNotifications()
	{
		return _messageContainer.GetAll();
	}

	public IEnumerable<NotyfNotification> ReadAllNotifications()
	{
		return _messageContainer.ReadAll();
	}

	public void RemoveAll()
	{
		_messageContainer.RemoveAll();
	}

	public void Custom(ToastNotificationType type, string message, int? duration = null, object? icon = null)
	{
		var toast = new NotyfNotification(type, message, duration, icon);
		_messageContainer.Add(toast);
	}

	public void Error(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Error, message, duration);
		_messageContainer.Add(toast);
	}

	public void Information(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Information, message, duration);
		_messageContainer.Add(toast);
	}

	public void Success(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Success, message, duration);
		_messageContainer.Add(toast);
	}

	public void Warning(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Warning, message, duration);
		_messageContainer.Add(toast);
	}
}