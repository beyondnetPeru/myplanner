
namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeRequest : IRequest<bool>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ICollection<CreateSizeModelTypeFactorRequest> Factors { get; set; }
        public string UserId { get; private set; }

        public CreateSizeModelTypeRequest(string code,
                                          string name,
                                          string description,
                                          ICollection<CreateSizeModelTypeFactorRequest> factors,
                                          string userId)
        {
            Code = code;
            Name = name;
            Factors = factors;
            Description = description;
            UserId = userId;
        }
    }

    public class CreateSizeModelTypeFactorRequest
    {
        public CreateSizeModelTypeFactorRequest(string code, string name, string userId)
        {
            Code = code;
            Name = name;
            UserId = userId;
        }

        public string Code { get; }
        public string Name { get; }
        public string UserId { get; }
    }
}
