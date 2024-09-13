using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType
{
    public class CreateSizeModelTypeRequest : IRequest<bool>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string UserId { get; private set; }

        public ICollection<AddSizeModelTypeFactorDto> Factors { get; set; }

        public CreateSizeModelTypeRequest(string code, string name, string description, ICollection<AddSizeModelTypeFactorDto> factors, string userId)
        {
            Code = code;
            Name = name;
            Description = description;
            Factors = factors;
            UserId = userId;
        }
    }
}
