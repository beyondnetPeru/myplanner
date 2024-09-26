using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries;
using MyPlanner.Plannings.Domain.SizeModelTypes;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes
{
    public class SizeModelTypeService(
      IMediator mediator,
      ISizeModelTypeRepository sizeModelTypeRepository,
      ISizeModelTypeQueryRepository sizeModelTypeQueryRepository,

      IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public ISizeModelTypeRepository SizeModelTypeRepository { get; } = sizeModelTypeRepository;
        public ISizeModelTypeQueryRepository SizeModelTypeQueryRepository { get; } = sizeModelTypeQueryRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
