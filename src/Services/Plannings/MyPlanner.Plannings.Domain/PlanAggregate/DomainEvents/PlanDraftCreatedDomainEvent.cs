namespace MyPlanner.Plannings.Domain.PlanAggregate.DomainEvents
{
    public record PlanDraftCreatedDomainEvent(string PlanId, 
                                         string PlanCode, 
                                         string PlanName, 
                                         string PlaNOwner, 
                                         DateTime CreatedDate,
                                         string eventName, int PlanStatus = 1) : DomainEvent(eventName);
}
