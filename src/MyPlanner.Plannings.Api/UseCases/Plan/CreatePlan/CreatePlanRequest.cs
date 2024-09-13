using MediatR;


namespace MyPlanner.Plannings.Api.UseCases.Plan.CreatePlan
{
    public class CreatePlanRequest : IRequest<bool>
    {

        public string Name { get; set; }
        public string Owner { get; set; }
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeName { get; set; }
        public string UserId { get; }

        public CreatePlanRequest(string name, string owner, string sizeModelTypeId, string sizeModelTypeName, string userId)
        {
            Name = name;
            Owner = owner;
            SizeModelTypeId = sizeModelTypeId;
            SizeModelTypeName = sizeModelTypeName;
            UserId = userId;
        }
    }
}
