using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetAllSizeModels
{
    public class GetAllSizeModelsQueryHandler : IRequestHandler<GetAllSizeModelsQuery, IEnumerable<SizeModelDto>>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;

        public GetAllSizeModelsQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
        }

        public async Task<IEnumerable<SizeModelDto>> Handle(GetAllSizeModelsQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelQueryRepository.GetAll(request.Pagination);
        }
    }
}
