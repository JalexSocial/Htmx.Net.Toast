﻿using Htmx.Net.Toast.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Htmx.Net.Toast.Containers;

public class TempDataToastNotificationContainer<TMessage> : IToastNotificationContainer<TMessage>
	where TMessage : class
{
	private const string Key = "AspNetCoreHero.ToastNotification";
	private readonly ITempDataService _tempDataWrapper;

	public TempDataToastNotificationContainer(ITempDataService tempDataWrapper)
	{
		_tempDataWrapper = tempDataWrapper;
	}

	public void Add(TMessage message)
	{
		var messages = _tempDataWrapper.Get<IEnumerable<TMessage>>(Key) ?? new List<TMessage>();
		var messagesList = messages.ToList();
		messagesList.Add(message);
		_tempDataWrapper.Add(Key, messagesList);
	}

	public void RemoveAll()
	{
		_tempDataWrapper.Remove(Key);
	}

	public IList<TMessage> GetAll()
	{
		return _tempDataWrapper.Peek<List<TMessage>>(Key) ?? new List<TMessage>();
	}

	public IList<TMessage> ReadAll()
	{
		var messages = GetAll();
		RemoveAll();
		return messages;
	}
}