using Htmx.Net.Toast.Abstractions;
using Htmx.Net.Toast.Containers;
using Htmx.Net.Toast.Enums;
using Htmx.Net.Toast.Notyf.Models;
using System.Collections.Generic;

namespace Htmx.Net.Toast.Notyf;

public class NotyfService : INotyfService
{
	protected IToastNotificationContainer<NotyfNotification> MessageContainer;

	public NotyfService(IMessageContainerFactory messageContainerFactory)
	{
		MessageContainer = messageContainerFactory.Create<NotyfNotification>();
	}

	public void Custom(ToastNotificationType type, string message, int? duration = null)
	{
		var toast = new NotyfNotification(type, message, duration);
		MessageContainer.Add(toast);
	}

	public void Error(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Error, message, duration);
		MessageContainer.Add(toast);
	}

	public IEnumerable<NotyfNotification> GetNotifications()
	{
		return MessageContainer.GetAll();
	}

	public void Information(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Information, message, duration);
		MessageContainer.Add(toast);
	}

	public IEnumerable<NotyfNotification> ReadAllNotifications()
	{
		return MessageContainer.ReadAll();
	}

	public void RemoveAll()
	{
		MessageContainer.RemoveAll();
	}

	public void Success(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Success, message, duration);
		MessageContainer.Add(toast);
	}

	public void Warning(string message, int? duration = null)
	{
		var toast = new NotyfNotification(ToastNotificationType.Warning, message, duration);
		MessageContainer.Add(toast);
	}
}