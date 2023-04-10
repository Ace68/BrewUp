using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using System.Collections.ObjectModel;

namespace Muflone.Transport.InMemory;

public static class MufloneBroker
{
	public static ObservableCollection<ICommand> Commands = new();
	public static ObservableCollection<IEvent> DomainEvents = new();
	public static ObservableCollection<IEvent> IntegrationEvents = new();

	public static void Send(ICommand command)
	{
		var exist = Commands.FirstOrDefault(x => x.MessageId.Equals(command.MessageId));
		if (exist != null)
			return;

		Commands.Add(command);
	}

	public static void Publish(IEvent @event)
	{
		if (@event.GetType() == typeof(IDomainEvent))
		{
			var exist = DomainEvents.FirstOrDefault(x => x.MessageId.Equals(@event.MessageId));
			if (exist != null)
				return;

			DomainEvents.Add(@event);
		}

		if (@event.GetType() == typeof(IIntegrationEvent))
		{
			var exist = IntegrationEvents.FirstOrDefault(x => x.MessageId.Equals(@event.MessageId));
			if (exist != null)
				return;
			IntegrationEvents.Add(@event);
		}
	}
}