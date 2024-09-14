using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes
{
    public class SizeModelTypeService(
      IMediator mediator,
      ISizeModelTypeRepository sizeModelTypeRepository,
      ISizeModelTypeFactorRepository sizeModelTypeFactorRepository,
      IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public ISizeModelTypeRepository SizeModelTypeRepository { get; } = sizeModelTypeRepository;
        public ISizeModelTypeFactorRepository SizeModelTypeFactorRepository { get; } = sizeModelTypeFactorRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
