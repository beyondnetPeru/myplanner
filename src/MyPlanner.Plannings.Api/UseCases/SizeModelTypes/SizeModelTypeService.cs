using MyPlanner.Plannings.Api.Endpoints;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries;


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
