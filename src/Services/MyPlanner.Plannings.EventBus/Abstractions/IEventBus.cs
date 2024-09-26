using System.Threading.Tasks;

namespace MyPlanner.Plannings.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
