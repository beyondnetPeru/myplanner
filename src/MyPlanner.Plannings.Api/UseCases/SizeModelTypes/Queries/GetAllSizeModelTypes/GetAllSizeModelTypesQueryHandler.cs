using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypes
{
    public class GetAllSizeModelTypesQueryHandler : IRequestHandler<GetAllSizeModelTypesQuery, IEnumerable<SizeModelTypeDto>>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetAllSizeModelTypesQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public async Task<IEnumerable<SizeModelTypeDto>> Handle(GetAllSizeModelTypesQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelTypeQueryRepository.GetAll(request.Pagination);
        }
    }
}
