using MyPlanner.Plannings.Api.Dtos.SizeModelType;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeItemsQueryHandler : IRequestHandler<GetAllSizeModelTypeItemsQuery, IEnumerable<SizeModelTypeItemDto>>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetAllSizeModelTypeItemsQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public async Task<IEnumerable<SizeModelTypeItemDto>> Handle(GetAllSizeModelTypeItemsQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelTypeQueryRepository.GetItems(request.SizeModelTypeId);
        }
    }
}
