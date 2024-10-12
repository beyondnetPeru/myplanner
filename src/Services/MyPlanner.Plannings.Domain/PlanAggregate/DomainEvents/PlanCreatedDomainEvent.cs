namespace MyPlanner.Plannings.Domain.PlanAggregate.DomainEvents
{
    public record PlanCreatedDomainEvent(string planId, 
                                         string planCode, 
                                         string PlanName, 
                                         string PlamOwner, 
                                         DateTime CreatedDate) : DomainEvent
    {
    }
}
