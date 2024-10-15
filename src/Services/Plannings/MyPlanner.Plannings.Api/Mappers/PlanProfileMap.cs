using MyPlanner.Plannings.Api.UseCases.Plan.Command.CreatePlan;
using MyPlanner.Plannings.Domain.PlanAggregate;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Shared.Domain.ValueObjects;
using MyPlanner.Shared.Infrastructure.Database;
using MyPlanner.Shared.Mappers.Converters;

namespace MyPlanner.Plannings.Api.Mappers
{

    public class PlanProfileMap : Profile
    {
        public PlanProfileMap()
        {

            CreateMap<int, PlanStatus>().ConvertUsing<EnumerationConverter<PlanStatus>>();
            CreateMap<int, PlanItemStatus>().ConvertUsing<EnumerationConverter<PlanItemStatus>>();            
            CreateMap<AuditDto, Audit>().ConvertUsing(new AuditDtoToEntityConvert());
            CreateMap<Audit, AuditDto>().ConvertUsing(new AuditToAuditDtoConvert());
            
            CreateMap<CreatePlanDto, CreatePlanCommand>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<PlanCategoriesDtoToCommandResolver>())
                .ForMember(dest => dest.Items, opt => opt.MapFrom<PlanItemsDtoToCommandResolver>());

            CreateMap<CreatePlanCommand, PlanProps>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<PlanCategoriesCommandToEntityResolver>())
                .ForMember(dest => dest.Items, opt => opt.MapFrom<PlanItemsCommandToEntityResolver>());     
            
            CreateMap<PlanCategory, PlanCategoryTable>()
                .ConstructUsing(p => new PlanCategoryTable() { Id = p.GetPropsCopy().Id.GetValue(), PlanId = p.GetPropsCopy().PlanId.GetValue(), Name = p.GetPropsCopy().Name.GetValue() });

            CreateMap<PlanCategoryTable, PlanCategory>()
                .ConstructUsing(p => PlanCategory.Create(IdValueObject.Create(p.Id), IdValueObject.Create(p.PlanId), Name.Create(p.Name)));

            CreateMap<Plan, PlanTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.GetPropsCopy().Id.GetValue()))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(p => p.GetPropsCopy().Owner.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(p => p.GetPropsCopy().Name.GetValue()))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom<PlanCategoriesEntityToTableResolver>())
                .ForMember(dest => dest.Items, opt => opt.MapFrom<PlanItemsEntityToTableResolver>())
                .ForMember(dest => dest.SizeModelTypeId, opt => opt.MapFrom(p => p.GetPropsCopy().SizeModelTypeId.GetValue()))
                .ForMember(dest => dest.Audit, opt => opt.MapFrom(p => p.GetPropsCopy().Audit))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(p => p.GetPropsCopy().Status.Id));

            CreateMap<PlanItem, PlanItemTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => p.GetPropsCopy().Id.GetValue()))
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(p => p.GetPropsCopy().PlanId.GetValue()))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(p => p.GetPropsCopy().ProductId.GetValue()))
                .ForMember(dest => dest.PlanCategoryId, opt => opt.MapFrom(p => p.GetPropsCopy().PlanCategoryId.GetValue()))
                .ForMember(dest => dest.BusinessFeatureName, opt => opt.MapFrom(p => p.GetPropsCopy().BusinessFeature.GetValue().Name))
                .ForMember(dest => dest.BusinessFeatureDefinition, opt => opt.MapFrom(p => p.GetPropsCopy().BusinessFeature.GetValue().BusinessDefinition))
                .ForMember(dest => dest.BusinessFeatureComplexityLevel, opt => opt.MapFrom(p => p.GetPropsCopy().BusinessFeature.GetValue().ComplexityLevel.Id))
                .ForMember(dest => dest.BusinessFeaturePriority, opt => opt.MapFrom(p => p.GetPropsCopy().BusinessFeature.GetValue().PriorityOrder))
                .ForMember(dest => dest.BusinessFeatureMoScoW, opt => opt.MapFrom(p => p.GetPropsCopy().BusinessFeature.GetValue().MoScoW.Id))
                .ForMember(dest => dest.TechnicalDefinition, opt => opt.MapFrom(p => p.GetPropsCopy().TechnicalDefinition.GetValue()))
                .ForMember(dest => dest.ComponentsImpacted, opt => opt.MapFrom(p => p.GetPropsCopy().ComponentsImpacted.GetValue()))
                .ForMember(dest => dest.TechnicalDependencies, opt => opt.MapFrom(p => p.GetPropsCopy().TechnicalDependencies.GetValue()))
                .ForMember(dest => dest.SizeModelTypeItemId, opt => opt.MapFrom(p => p.GetPropsCopy().SizeModelTypeItemId.GetValue()))
                .ForMember(dest => dest.BallParkCostSymbol, opt => opt.MapFrom(p => p.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Symbol.Id))
                .ForMember(dest => dest.BallParkCostAmount, opt => opt.MapFrom(p => p.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Amount))
                .ForMember(dest => dest.BallparkDependenciesCostAmount, opt => opt.MapFrom(p => p.GetPropsCopy().BallParkCosts.GetValue().BallparkDependenciesCost.GetValue().Amount))
                .ForMember(dest => dest.BallParkTotalCostAmount, opt => opt.MapFrom(p => p.GetPropsCopy().BallParkCosts.GetValue().BallParkTotalCost.GetValue().Amount))
                .ForMember(dest => dest.KeyAssumptions, opt => opt.MapFrom(p => p.GetPropsCopy().KeyAssumptions.GetValue()))
                .ForMember(dest => dest.Audit, opt => opt.MapFrom(p => p.GetPropsCopy().Audit))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(p => p.GetPropsCopy().Status.Id));


            CreateMap<PlanTable, Plan>()
                .ConstructUsing(dest => Plan.Load(new PlanProps(
                    IdValueObject.Create(dest.Id),
                    PlanCode.Create(dest.Code),
                    IdValueObject.Create(dest.SizeModelTypeId),
                    dest.Categories.Select(p => PlanCategory.Create(IdValueObject.Create(p.Id), IdValueObject.Create(p.PlanId), Name.Create(p.Name))).ToList(),
                    Name.Create(dest.Name),
                    Owner.Create(dest.Owner),
                    dest.Items.Select(
                        p => PlanItem.Load(
                            p.Id,
                            p.PlanItemType,
                            p.PlanId,
                            p.ProductId,
                            p.PlanCategoryId,
                            BusinessFeature.Create(p.BusinessFeatureName, 
                                                   p.BusinessFeatureDefinition, 
                                                   Enumeration.FromValue<ComplexityLevelEnum>(p.BusinessFeatureComplexityLevel), 
                                                   p.BusinessFeaturePriority, 
                                                   Enumeration.FromValue<MoScoWEnum>(p.BusinessFeatureMoScoW)),
                            TechnicalDefinition.Create(p.TechnicalDefinition),
                            ComponentsImpacted.Create(p.ComponentsImpacted),
                            TechnicalDependencies.Create(p.TechnicalDependencies),
                            p.SizeModelTypeItemId,
                            BallParkCost.Create(Enumeration.FromValue<CurrencySymbolEnum>(p.BallParkCostSymbol), p.BallParkCostAmount, p.BallparkDependenciesCostAmount),
                            KeyAssumptions.Create(p.KeyAssumptions),                            
                            Audit.Load(new AuditProps()
                            {
                                CreatedBy = p.Audit.CreatedBy,
                                CreatedAt = p.Audit.CreatedAt,
                                UpdatedBy = p.Audit.UpdatedBy,
                                UpdatedAt = p.Audit.UpdatedAt,
                                TimeSpan = p.Audit.TimeSpan
                            }),
                            p.Status,
                            p.Audit.CreatedBy
                        )).ToList()
                    ,
                    Audit.Load(new AuditProps()
                    {
                        CreatedBy = dest.Audit.CreatedBy,
                        CreatedAt = dest.Audit.CreatedAt,
                        UpdatedBy = dest.Audit.UpdatedBy,
                        UpdatedAt = dest.Audit.UpdatedAt,
                        TimeSpan = dest.Audit.TimeSpan
                    }),
                    Enumeration.FromValue<PlanStatus>(dest.Status)
                )));

            CreateMap<AddPlanItemDto, AddPlanItemCommand>();
            CreateMap<PlanTable, PlanDto>();
            CreateMap<PlanCategoryTable, PlanCategoryDto>();
            CreateMap<PlanItemTable, PlanItemDto>();
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

    internal class PlanCategoriesEntityToTableResolver : IValueResolver<Plan, PlanTable, ICollection<PlanCategoryTable>>
    {
        public ICollection<PlanCategoryTable> Resolve(Plan source, PlanTable destination, ICollection<PlanCategoryTable> destMember, ResolutionContext context)
        {
            var items = new List<PlanCategoryTable>();

            foreach (var category in source.GetPropsCopy().Categories)
            {
                var planCategoryEnity = new PlanCategoryTable() { Id = category.GetPropsCopy().Id.GetValue(), PlanId = source.GetPropsCopy().Id.GetValue(), Name = category.GetPropsCopy().Name.GetValue() };

                items.Add(planCategoryEnity);
            }

            return items;
        }
    }

    internal class PlanCategoriesTableToEntityResolver : IValueResolver<PlanTable, PlanProps, ICollection<PlanCategory>>
    {
        public ICollection<PlanCategory> Resolve(PlanTable source, PlanProps destination, ICollection<PlanCategory> destMember, ResolutionContext context)
        {
            var items = new List<PlanCategory>();

            foreach (var category in source.Categories)
            {
                var planCategoryProp = new PlanCategoryProps(IdValueObject.Create(category.Id),
                                                destination.Id,
                                                Name.Create(category.Name));

                var planCategory = PlanCategory.Create(planCategoryProp.Id, planCategoryProp.PlanId, planCategoryProp.Name);

                items.Add(planCategory);
            }

            return items;
        }
    }

    internal class PlanItemsEntityToTableResolver : IValueResolver<Plan, PlanTable, ICollection<PlanItemTable>>
    {
        public ICollection<PlanItemTable> Resolve(Plan source, PlanTable destination, ICollection<PlanItemTable> destMember, ResolutionContext context)
        {
            var items = new List<PlanItemTable>();


            foreach (var item in source.GetPropsCopy().Items)
            {
                var planItem = new PlanItemTable()
                {
                    Id = item.GetPropsCopy().Id.GetValue(),
                    PlanId = source.GetPropsCopy().Id.GetValue(),
                    ProductId = item.GetPropsCopy().ProductId.GetValue(),
                    PlanCategoryId = item.GetPropsCopy().PlanCategoryId.GetValue(),
                    BusinessFeatureName = item.GetPropsCopy().BusinessFeature.GetValue().Name,
                    BusinessFeatureDefinition = item.GetPropsCopy().BusinessFeature.GetValue().BusinessDefinition,
                    BusinessFeatureComplexityLevel = item.GetPropsCopy().BusinessFeature.GetValue().ComplexityLevel.Id,
                    BusinessFeaturePriority = item.GetPropsCopy().BusinessFeature.GetValue().PriorityOrder,
                    BusinessFeatureMoScoW = item.GetPropsCopy().BusinessFeature.GetValue().MoScoW.Id,
                    TechnicalDefinition = item.GetPropsCopy().TechnicalDefinition.GetValue(),
                    ComponentsImpacted = item.GetPropsCopy().ComponentsImpacted.GetValue(),
                    TechnicalDependencies = item.GetPropsCopy().TechnicalDependencies.GetValue(),
                    SizeModelTypeItemId = item.GetPropsCopy().SizeModelTypeItemId.GetValue(),
                    BallParkCostSymbol = item.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Symbol.Id,
                    BallParkCostAmount = item.GetPropsCopy().BallParkCosts.GetValue().BallParkCost.GetValue().Amount,
                    BallparkDependenciesCostAmount = item.GetPropsCopy().BallParkCosts.GetValue().BallparkDependenciesCost.GetValue().Amount,
                    BallParkTotalCostAmount = item.GetPropsCopy().BallParkCosts.GetValue().BallParkTotalCost.GetValue().Amount,
                    KeyAssumptions = item.GetPropsCopy().KeyAssumptions.GetValue(),
                    Audit = new AuditTable()
                    {
                        CreatedBy = item.GetPropsCopy().Audit.GetValue().CreatedBy,
                        CreatedAt = item.GetPropsCopy().Audit.GetValue().CreatedAt,
                        UpdatedBy = item.GetPropsCopy().Audit.GetValue().UpdatedBy,
                        UpdatedAt = item.GetPropsCopy().Audit.GetValue().UpdatedAt,
                        TimeSpan = item.GetPropsCopy().Audit.GetValue().TimeSpan
                    },
                    Status = item.GetPropsCopy().Status.Id
                };

                items.Add(planItem);
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
                    Enumeration.FromValue<PlanItemTypeEnum>(item.PlanItemType)!,
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

    internal class PlanItemsTableToEntityResolver : IValueResolver<PlanTable, PlanProps, ICollection<PlanItem>>
    {
        public ICollection<PlanItem> Resolve(PlanTable source, PlanProps destination, ICollection<PlanItem> destMember, ResolutionContext context)
        {
            var items = new List<PlanItem>();


            foreach (var item in source.Items)
            {
                items.Add(PlanItem.Load(item.Id,
                                        item.PlanItemType,      
                                        item.PlanId,
                                        item.ProductId,
                                        item.PlanCategoryId,
                                        BusinessFeature.Create(item.BusinessFeatureName,
                                                               item.BusinessFeatureDefinition,
                                                               Enumeration.FromValue<ComplexityLevelEnum>(item.BusinessFeatureComplexityLevel),
                                                               item.BusinessFeaturePriority,
                                                               Enumeration.FromValue<MoScoWEnum>(item.BusinessFeatureMoScoW)),
                                        TechnicalDefinition.Create(item.TechnicalDefinition),
                                        ComponentsImpacted.Create(item.ComponentsImpacted),
                                        TechnicalDependencies.Create(item.TechnicalDependencies),
                                        item.SizeModelTypeItemId,
                                        BallParkCost.Create(Enumeration.FromValue<CurrencySymbolEnum>(item.BallParkCostSymbol), item.BallParkCostAmount, item.BallparkDependenciesCostAmount),
                                        KeyAssumptions.Create(item.KeyAssumptions),
                                        Audit.Load(new AuditProps()
                                        {
                                            CreatedBy = item.Audit.CreatedBy,
                                            CreatedAt = item.Audit.CreatedAt,
                                            UpdatedBy = item.Audit.UpdatedBy,
                                            UpdatedAt = item.Audit.UpdatedAt,
                                            TimeSpan = item.Audit.TimeSpan
                                        }),
                                        item.Status,
                                        item.Audit.CreatedBy));
            }

            return items;
        }
    }

    #endregion
}
