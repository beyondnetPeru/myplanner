
using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Database;

namespace MyPlanner.Plannings.Api.Mappers
{

    public class PlanProfileMap : Profile
    {
        public PlanProfileMap()
        {
            CreateMap<int, PlanStatus>().ConvertUsing<EnumerationConverter<PlanStatus>>();
            CreateMap<int, PlanItemStatus>().ConvertUsing<EnumerationConverter<PlanItemStatus>>();
            CreateMap<Audit, AuditTable>().ConvertUsing(new AuditToAuditTableConvert());

            CreateMap<CreatePlanDto, CreatePlanCommand>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<PlanCategoriesDtoToCommandResolver>())
                .ForMember(dest => dest.Items, opt => opt.MapFrom<PlanItemsDtoToCommandResolver>());

            CreateMap<CreatePlanCommand, PlanProps>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<PlanCategoriesCommandToEntityResolver>())
                .ForMember(dest => dest.Items, opt => opt.MapFrom<PlanItemsCommandToEntityResolver>());


            CreateMap<PlanTable, PlanDto>();
            CreateMap<Plan, PlanTable>();
        }
    }

    #region Resolvers

    internal class PlanCategoriesDtoToCommandResolver : IValueResolver<CreatePlanDto, CreatePlanCommand, ICollection<CreatePlanCategoryCommand>>
    {
        public ICollection<CreatePlanCategoryCommand> Resolve(CreatePlanDto source, CreatePlanCommand destination, ICollection<CreatePlanCategoryCommand> destMember, ResolutionContext context)
        {
            var items = new List<CreatePlanCategoryCommand>();

            foreach (var category in source.Categories)
            {
                items.Add(new CreatePlanCategoryCommand { Name = category.Name });
            }

            return items;
        }
    }

    internal class PlanCategoriesCommandToEntityResolver : IValueResolver<CreatePlanCommand, PlanProps, ICollection<PlanCategory>>
    {
        public ICollection<PlanCategory> Resolve(CreatePlanCommand source, PlanProps destination, ICollection<PlanCategory> destMember, ResolutionContext context)
        {
            var items = new List<PlanCategory>();

            foreach (var category in source.Categories)
            {
                var planCategoryEnity = PlanCategory.Create(IdValueObject.Create(), destination.Id, Name.Create(category.Name));

                items.Add(planCategoryEnity);
            }

            return items;
        }
    }

    internal class PlanItemsCommandToEntityResolver : IValueResolver<CreatePlanCommand, PlanProps, ICollection<PlanItem>>
    {
        public ICollection<PlanItem> Resolve(CreatePlanCommand source, PlanProps destination, ICollection<PlanItem> destMember, ResolutionContext context)
        {
            var items = new List<PlanItem>();
            

            foreach (var item in source.Items)
            {
                var categoryId = destination.Categories.FirstOrDefault(x => x.GetPropsCopy().Name.GetValue().ToLowerInvariant() == item.PlanCategoryName.ToLowerInvariant())!.GetPropsCopy().Id.GetValue() ?? string.Empty;

                var planItem = PlanItem.Create(
                    IdValueObject.Create(),
                    destination.Id,
                    IdValueObject.Create(item.ProductId),
                    IdValueObject.Create(categoryId),
                    BusinessFeature.Create(item.BusinessFeatureName, item.BusinessFeatureDefinition, item.BusinessFeatureComplexityLevel, item.BusinessFeaturePriority, item.BusinessFeatureMoScoW),
                    TechnicalDefinition.Create(item.TechnicalDefinition),
                    ComponentsImpacted.Create(item.ComponentsImpacted),
                    TechnicalDependencies.Create(item.TechnicalDependencies),
                    IdValueObject.Create(item.SizeModelTypeItemId),
                    BallParkCost.Create(item.BallParkCostSymbol, item.BallParkCostAmount, item.BallparkDependenciesCostAmount),
                    KeyAssumptions.Create(item.KeyAssumptions),
                    UserId.Create(item.UserId)
                );                 

                items.Add(planItem);
            }

            return items;
        }
    }
    
    internal class PlanItemsDtoToCommandResolver : IValueResolver<CreatePlanDto, CreatePlanCommand, ICollection<CreatePlanItemCommand>>
    {
        public ICollection<CreatePlanItemCommand> Resolve(CreatePlanDto source, CreatePlanCommand destination, ICollection<CreatePlanItemCommand> destMember, ResolutionContext context)
        {
            var items = new List<CreatePlanItemCommand>();

            foreach (var item in source.Items)
            {
                var planItem = new CreatePlanItemCommand()
                {
                    ProductId = item.ProductId,
                    PlanCategoryName = item.PlanCategoryName,
                    BusinessFeatureName = item.BusinessFeatureName,
                    BusinessFeatureDefinition = item.BusinessFeatureDefinition,
                    BusinessFeatureComplexityLevel = Enumeration.FromValue<ComplexityLevelEnum>(item.BusinessFeatureComplexityLevel),
                    BusinessFeaturePriority = item.BusinessFeaturePriority,
                    BusinessFeatureMoScoW = Enumeration.FromValue<MoScoWEnum>(item.BusinessFeatureMoScoW),
                    TechnicalDefinition = item.TechnicalDefinition,
                    ComponentsImpacted = item.ComponentsImpacted,
                    TechnicalDependencies = item.TechnicalDependencies,
                    SizeModelTypeItemId = item.SizeModelTypeItemId,
                    BallParkCostSymbol = Enumeration.FromValue<CurrencySymbolEnum>(item.BallParkCostSymbol),
                    BallParkCostAmount = item.BallParkCostAmount,
                    BallparkDependenciesCostAmount = item.BallparkDependenciesCostAmount,
                    BallParkTotalCostAmount = item.BallParkTotalCostAmount,
                    KeyAssumptions = item.KeyAssumptions,
                    UserId = item.UserId
                };

                items.Add(planItem);
            }

            return items;
        }
    }


    #endregion
}
