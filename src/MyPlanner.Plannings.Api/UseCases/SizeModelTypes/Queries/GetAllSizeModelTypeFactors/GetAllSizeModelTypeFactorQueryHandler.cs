using MyPlanner.Plannings.Api.Dtos.SizeModelType;


namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorQueryHandler : IRequestHandler<GetAllSizeModelTypeFactorQuery, IEnumerable<SizeModelTypeFactorDto>>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetAllSizeModelTypeFactorQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public async Task<IEnumerable<SizeModelTypeFactorDto>> Handle(GetAllSizeModelTypeFactorQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelTypeQueryRepository.GetFactors(request.SizeModelTypeId);
        }
    }
}
