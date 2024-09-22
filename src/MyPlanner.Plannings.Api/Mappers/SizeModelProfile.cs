using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Plannings.Shared.Application.Dtos;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;
using MyPlanner.Plannings.Shared.Infrastructure.Database;


namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelItemPropsToTableResolver : IValueResolver<SizeModelProps, SizeModelTable, ICollection<SizeModelItemTable>>
    {
        public ICollection<SizeModelItemTable> Resolve(SizeModelProps source, SizeModelTable destination, ICollection<SizeModelItemTable> destMember, ResolutionContext context)
        {
            var items = new List<SizeModelItemTable>();

            if (destination.Items == null)
            {
                destination.Items = new List<SizeModelItemTable>();
            }

            source.Items.ToList().ForEach(item =>
            {
                var sizeModelItemTable = new SizeModelItemTable()
                {
                    Id = item.GetPropsCopy().Id.GetValue(),
                    SizeModelId = item.GetPropsCopy().Id.GetValue(),
                    SizeModel = destination,
                    SizeModelTypeItemId = item.GetPropsCopy().SizeModelTypeItem.GetPropsCopy().Id.GetValue(),
                    FactorSelected = item.GetPropsCopy().FactorSelected.Id,
                    ProfileName = item.GetPropsCopy().Profile.GetValue().ProfileName.GetValue(),
                    ProfileAvgRateSymbol = item.GetPropsCopy().Profile.GetValue().ProfileAvgRate.GetValue().Symbol.Id,
                    ProfileAvgRateValue = item.GetPropsCopy().Profile.GetValue().ProfileAvgRate.GetValue().Value,
                    SizeModelTypeSelected = item.GetPropsCopy().SizeModelTypeSelected.GetValue(),
                    Quantity = item.GetPropsCopy().Quantity.GetValue(),
                    TotalCost = item.GetPropsCopy().TotalCost.GetValue(),
                    IsStandard = item.GetPropsCopy().IsStandard.GetValue(),
                    Status = item.GetPropsCopy().Status.Id,
                    Audit = new AuditTable()
                    {
                        CreatedBy = item.GetPropsCopy().Audit.GetValue().CreatedBy,
                        CreatedAt = item.GetPropsCopy().Audit.GetValue().CreatedAt,
                        UpdatedBy = item.GetPropsCopy().Audit.GetValue().UpdatedBy!,
                        UpdatedAt = item.GetPropsCopy().Audit.GetValue().UpdatedAt,
                        TimeSpan = item.GetPropsCopy().Audit.GetValue().TimeSpan
                    }
                };

                destination.Items.Add(sizeModelItemTable);
            });

            return destination.Items;
        }
    }

    public class AuditToAuditTableConvert : ITypeConverter<Audit, AuditTable>
    {
        public AuditTable Convert(Audit source, AuditTable destination, ResolutionContext context)
        {
            var dto = new AuditTable
            {
                CreatedBy = source.GetValue().CreatedBy,
                CreatedAt = source.GetValue().CreatedAt,
                UpdatedBy = source.GetValue().UpdatedBy!,
                UpdatedAt = source.GetValue().UpdatedAt,
                TimeSpan = source.GetValue().TimeSpan
            };

            return dto;
        }
    }

    public class SizeModelProfile : Profile
    {
        public SizeModelProfile()
        {
            // SizeModel
            CreateMap<CreateSizeModelDto, CreateSizeModelRequest>();
            CreateMap<SizeModelTable, SizeModelDto>();
            CreateMap<Audit, AuditTable>().ConvertUsing(new AuditToAuditTableConvert());

            CreateMap<SizeModelProps, SizeModelTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(new SizeModelItemPropsToTableResolver()));

            //SizeModelItem
            CreateMap<CreateSizeModelItemDto, CreateSizeModelItemRequest>();

        }
    }
}
