using MediatR;
using System.Collections;

namespace MyPlanner.Shared.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, object entity)
        {
            ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));

            var type = entity.GetType();

            var domainEvents = type.GetProperty("DomainEvents")?.GetValue(entity) as IEnumerable;

            if (domainEvents != null)
            {
                if (domainEvents == null)
                {
                    return;
                }

                foreach (var domainEvent in domainEvents)
                {
                    await mediator.Publish(domainEvent);
                }

                var clearDomainEvents = type.GetMethod("ClearDomainEvents");

                if (clearDomainEvents != null)
                {
                    clearDomainEvents.Invoke(entity, null);
                }

            }
        }
    }
}
