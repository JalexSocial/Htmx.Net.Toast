using Htmx.Net.Toast.Abstractions;

namespace Htmx.Net.Toast.Containers;

public interface IMessageContainerFactory
{
	IToastNotificationContainer<TMessage> Create<TMessage>() where TMessage : class;
}