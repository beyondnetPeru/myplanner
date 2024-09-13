using AutoMapper;
using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.GetAllSizeModelTypeFactors
{
    public class GetAllSizeModelTypeFactorQueryHandler : IRequestHandler<GetAllSizeModelTypeFactorQuery, IEnumerable<SizeModelTypeFactorDto>>
    {
        private readonly ISizeModelTypeFactorRepository sizeModelTypeFactorRepository;
        private readonly IMapper mapper;

        public GetAllSizeModelTypeFactorQueryHandler(ISizeModelTypeFactorRepository sizeModelTypeFactorRepository, IMapper mapper)
        {
            this.sizeModelTypeFactorRepository = sizeModelTypeFactorRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeFactorRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<SizeModelTypeFactorDto>> Handle(GetAllSizeModelTypeFactorQuery request, CancellationToken cancellationToken)
        {
            var sizeModelTypeFactors = await sizeModelTypeFactorRepository.GetAll(request.SizeModelType);

            var dtos = mapper.Map<IEnumerable<SizeModelTypeFactorDto>>(sizeModelTypeFactors);

            return dtos;
        }
    }
}
