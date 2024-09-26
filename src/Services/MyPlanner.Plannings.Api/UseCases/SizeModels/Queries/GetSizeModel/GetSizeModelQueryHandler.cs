using MyPlanner.Plannings.Api.Dtos.SizeModel;


namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Queries.GetSizeModel
{
    public class GetSizeModelQueryHandler : IRequestHandler<GetSizeModelQuery, SizeModelDto>
    {
        private readonly ISizeModelQueryRepository sizeModelQueryRepository;

        public GetSizeModelQueryHandler(ISizeModelQueryRepository sizeModelQueryRepository)
        {
            this.sizeModelQueryRepository = sizeModelQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelQueryRepository));
        }

        public async Task<SizeModelDto> Handle(GetSizeModelQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelQueryRepository.Get(request.SizeModelId);
        }
    }
}
