using MyPlanner.Plannings.Api.Dtos.SizeModelType;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Queries.GetSizeModelTypeById
{
    public class GetSizeModelTypeByIdQueryHandler : IRequestHandler<GetSizeModelTypeByIdQuery, SizeModelTypeDto>
    {
        private readonly ISizeModelTypeQueryRepository sizeModelTypeQueryRepository;

        public GetSizeModelTypeByIdQueryHandler(ISizeModelTypeQueryRepository sizeModelTypeQueryRepository)
        {
            this.sizeModelTypeQueryRepository = sizeModelTypeQueryRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeQueryRepository));
        }

        public async Task<SizeModelTypeDto> Handle(GetSizeModelTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await sizeModelTypeQueryRepository.GetById(request.Id);
        }
    }
}
