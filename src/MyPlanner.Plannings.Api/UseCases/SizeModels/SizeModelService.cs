using MyPlanner.Plannings.Api.UseCases.SizeModels.Queries;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels
{
    public class SizeModelService(
      IMediator mediator,
      ISizeModelRepository sizeModelRepository,
      ISizeModelQueryRepository sizeModelQueryRepository,
      IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public ISizeModelRepository SizeModelRepository { get; } = sizeModelRepository;
        public ISizeModelQueryRepository SizeModelQueryRepository { get; } = sizeModelQueryRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
