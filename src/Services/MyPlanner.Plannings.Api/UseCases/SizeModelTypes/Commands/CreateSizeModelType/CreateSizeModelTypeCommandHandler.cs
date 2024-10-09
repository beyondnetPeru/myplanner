using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Shared.Cqrs;
using MyPlanner.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType
{
    public class CreateSizeModelTypeCommandHandler : AbstractCommandHandler<CreateSizeModelTypeCommand, ResultSet>
    {
        private readonly ISizeModelTypeRepository sizeModelTypeRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CreateSizeModelTypeCommandHandler(ISizeModelTypeRepository sizeModelTypeRepository,
                                                 ILogger<CreateSizeModelTypeCommandHandler> logger,
                                                 IMediator mediator,
                                                 IMapper mapper) : base(logger) 
        {
            this.sizeModelTypeRepository = sizeModelTypeRepository ?? throw new ArgumentNullException(nameof(sizeModelTypeRepository));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResultSet> HandleCommand(CreateSizeModelTypeCommand request, CancellationToken cancellationToken)
        {

            var props = mapper.Map<SizeModelTypeProps>(request);

            var entity = SizeModelType.Create(IdValueObject.Create(), props.Code, props.Name);

            if (!entity.IsValid())
            {
                return ResultSet.Error($"SizeModelType is not valid. Errors: {entity.GetBrokenRules().ToString()}");
            }

            if (request.Items.Any())
            {
                foreach (var item in request.Items)
                {
                    var factor = SizeModelTypeItem.Create(IdValueObject.Create(),
                                                            SizeModelTypeItemCode.Create(item.Code),
                                                            Name.Create(item.Name),
                                                            entity);

                    if (!factor.IsValid())
                    {
                        return ResultSet.Error($"SizeModelTypeFactor is not valid. Errors: {factor.GetBrokenRules().ToString()}");
                    }

                    entity.AddItem(factor);
                }
            }

            sizeModelTypeRepository.Add(entity);

            await sizeModelTypeRepository.UnitOfWork.SaveEntitiesAsync(entity, cancellationToken);

            return ResultSet.Success();

        }
    }
}
