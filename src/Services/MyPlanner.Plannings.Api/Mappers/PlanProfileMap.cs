using MyPlanner.Plannings.Api.Dtos.Plan;
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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.SizeModelTypeId, opt => opt.MapFrom(src => src.SizeModelTypeId))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));


            CreateMap<CreatePlanCategoryDto, CreatePlanCategoryCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<CreatePlanItemDto, CreatePlanItemCommand>()
                .ForMember(dest => dest.PlanId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.PlanCategoryId, opt => opt.MapFrom(src => src.PlanCategoryId))
                .ForMember(dest => dest.BusinessFeatureName, opt => opt.MapFrom(src => src.BusinessFeatureName))
                .ForMember(dest => dest.BusinessFeatureDefinition, opt => opt.MapFrom(src => src.BusinessFeatureDefinition))
                .ForMember(dest => dest.BusinessFeatureComplexityLevel, opt => opt.MapFrom(src => src.BusinessFeatureComplexityLevel))
                .ForMember(dest => dest.BusinessFeaturePriority, opt => opt.MapFrom(src => src.BusinessFeaturePriority))
                .ForMember(dest => dest.BusinessFeatureMoScoW, opt => opt.MapFrom(src => src.BusinessFeatureMoScoW))
                .ForMember(dest => dest.TechnicalDefinition, opt => opt.MapFrom(src => src.TechnicalDefinition))
                .ForMember(dest => dest.ComponentsImpacted, opt => opt.MapFrom(src => src.ComponentsImpacted))
                .ForMember(dest => dest.TechnicalDependencies, opt => opt.MapFrom(src => src.TechnicalDependencies))
                .ForMember(dest => dest.SizeModelTypeItemId, opt => opt.MapFrom(src => src.SizeModelTypeItemId))
                .ForMember(dest => dest.BallParkCostSymbol, opt => opt.MapFrom(src => src.BallParkCostSymbol))
                .ForMember(dest => dest.BallParkCostAmount, opt => opt.MapFrom(src => src.BallParkCostAmount))
                .ForMember(dest => dest.BallparkDependenciesCostAmount, opt => opt.MapFrom(src => src.BallparkDependenciesCostAmount))
                .ForMember(dest => dest.BallParkTotalCostAmount, opt => opt.MapFrom(src => src.BallParkTotalCostAmount))
                .ForMember(dest => dest.KeyAssumptions, opt => opt.MapFrom(src => src.KeyAssumptions))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            
            CreateMap<PlanTable, PlanDto>();
            CreateMap<Plan, PlanTable>();
        }
    }
}
