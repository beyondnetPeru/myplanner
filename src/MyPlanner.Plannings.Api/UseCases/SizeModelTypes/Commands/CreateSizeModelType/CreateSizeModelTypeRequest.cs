using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.AddSizeModelTypeFactor;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeRequest : IRequest<bool>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<AddSizeModelTypeFactorRequest> Factors { get; set; }
        public string UserId { get; private set; }

        public CreateSizeModelTypeRequest(string code, string name, string description, ICollection<AddSizeModelTypeFactorRequest> factors, string userId)
        {
            Code = code;
            Name = name;
            Factors = factors;
            Description = description;
            UserId = userId;
        }
    }
}
