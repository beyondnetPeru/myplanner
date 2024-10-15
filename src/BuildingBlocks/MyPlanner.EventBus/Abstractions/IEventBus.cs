using System.Threading.Tasks;

namespace MyPlanner.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
