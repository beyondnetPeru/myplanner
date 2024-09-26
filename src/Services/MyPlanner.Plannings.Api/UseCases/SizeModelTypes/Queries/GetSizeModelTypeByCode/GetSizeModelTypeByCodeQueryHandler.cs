using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeByCode
{
    public class GetSizeModelTypeByCodeQueryHandler : IRequestHandler<GetSizeModelTypeByCodeQuery, SizeModelTypeDto>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeRepository;

        public GetSizeModelTypeByCodeQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeRepository)
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
        }

        public async Task<SizeModelTypeDto> Handle(GetSizeModelTypeByCodeQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelTypeRepository.GetByCode(request.Code);
        }
    }
}
