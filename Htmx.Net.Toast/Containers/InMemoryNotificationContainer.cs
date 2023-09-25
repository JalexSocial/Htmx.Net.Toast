using Htmx.Net.Toast.Abstractions;
using System.Collections.Generic;

namespace Htmx.Net.Toast.Containers;

/// <summary>
///     This container is used for ajax calls.
/// </summary>
/// <typeparam name="TMessage"></typeparam>
public class InMemoryNotificationContainer<TMessage> : IToastNotificationContainer<TMessage> where TMessage : class
{
	public InMemoryNotificationContainer()
	{
		Messages = new List<TMessage>();
	}

	private IList<TMessage> Messages { get; }

	public void Add(TMessage message)
	{
		Messages.Add(message);
	}

	public void RemoveAll()
	{
		Messages.Clear();
	}

	public IList<TMessage> GetAll()
	{
		return Messages;
	}

	public IList<TMessage> ReadAll()
	{
		var messages = new List<TMessage>(Messages);
		Messages.Clear();
		return messages;
	}
}