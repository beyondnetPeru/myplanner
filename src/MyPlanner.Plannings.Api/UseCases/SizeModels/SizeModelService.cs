using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels
{
    public class SizeModelService(
      IMediator mediator,
      ISizeModelRepository sizeModelRepository,
      IMapper mapper)
    {
        public IMediator Mediator { get; } = mediator;
        public ISizeModelRepository SizeModelRepository { get; } = sizeModelRepository;
        public IMapper Mapper { get; } = mapper;
    }
}
