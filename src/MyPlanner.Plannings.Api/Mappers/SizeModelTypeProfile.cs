using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;
using static MyPlanner.Plannings.Domain.SizeModels.SizeModelTypeItem;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelTypeItemPropsToTableResolver : IValueResolver<SizeModelTypeProps, SizeModelTypeTable, ICollection<SizeModelTypeItemTable>>
    {
        public ICollection<SizeModelTypeItemTable> Resolve(SizeModelTypeProps source, SizeModelTypeTable destination, ICollection<SizeModelTypeItemTable> destMember, ResolutionContext context)
        {
            var items = new List<SizeModelTypeItemTable>();

            if (destination.Items == null)
            {
                destination.Items = new List<SizeModelTypeItemTable>();
            }

            source.Items.ToList().ForEach(item =>
            {
                var factor = new SizeModelTypeItemTable
                {
                    Id = item.GetPropsCopy().Id.GetValue(),
                    Code = item.GetPropsCopy().Code.GetValue(),
                    Name = item.GetPropsCopy().Name.GetValue(),
                    Status = item.GetPropsCopy().Status.Id,
                    SizeModelType = null
                };

                destination.Items.Add(factor);
            });

            return destination.Items;
        }
    }

    public class SizeModelTypeItemRequestToPropsResolver : IValueResolver<CreateSizeModelTypeRequest, SizeModelTypeProps, ICollection<SizeModelTypeItem>>
    {
        public ICollection<SizeModelTypeItem> Resolve(CreateSizeModelTypeRequest source, SizeModelTypeProps destination, ICollection<SizeModelTypeItem> destMember, ResolutionContext context)
        {
            var items = new List<SizeModelTypeItem>();

            source.Items.ToList().ForEach(item =>
            {
                var factor = SizeModelTypeItem.Create(IdValueObject.Create(),
                                                        SizeModelTypeItemCode.Create(item.Code),
                                                        Name.Create(item.Name),
                                                        null);

                items.Add(factor);
            });

            return items;
        }
    }

    public class SizeModelTypeEnumAction : IMappingAction<SizeModelTypeTable, SizeModelTypeDto>
    {
        public void Process(SizeModelTypeTable source, SizeModelTypeDto destination, ResolutionContext context)
        {
            destination.Status = Enumeration.FromValue<SizeModelTypeStatus>(source.Status).Name.ToUpper();
        }
    }

    public class SizeModelTypeItemEnumAction : IMappingAction<SizeModelTypeItemTable, SizeModelTypeItemDto>
    {
        public void Process(SizeModelTypeItemTable source, SizeModelTypeItemDto destination, ResolutionContext context)
        {
            destination.Status = Enumeration.FromValue<SizeModelTypeItemStatus>(source.Status).Name.ToUpper();
        }
    }

    public class SizeModelTypeProfile : Profile
    {
        public SizeModelTypeProfile()
        {
            // Size Model Types
            CreateMap<int, SizeModelTypeStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeStatus>>();
            CreateMap<int, SizeModelTypeItemStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeItemStatus>>();

            CreateMap<CreateSizeModelTypeDto, CreateSizeModelTypeRequest>();
            CreateMap<CreateSizeModelTypeItemDto, CreateSizeModelTypeItemsRequest>();
            CreateMap<ChangeNameSizeModelTypeDto, ChangeNameSizeModelTypeRequest>();
            CreateMap<ActivateSizeModelTypeDto, ActivateSizeModelTypeRequest>();
            CreateMap<DeactivateSizeModelTypeDto, DeactivateSizeModelTypeRequest>();
            CreateMap<ChangeCodeSizeModelTypeDto, ChangeCodeSizeModelTypeRequest>();

            CreateMap<SizeModelTypeTable, SizeModelTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).AfterMap<SizeModelTypeEnumAction>();


            CreateMap<CreateSizeModelTypeRequest, SizeModelTypeProps>()
                .ConstructUsing(src =>
                        new SizeModelTypeProps(IdValueObject.DefaultValue,
                                               SizeModelTypeCode.Create(src.Code),
                                               Name.Create(src.Name))).
                ForMember(dest => dest.Items, opt => opt.MapFrom(new SizeModelTypeItemRequestToPropsResolver()));


            CreateMap<SizeModelTypeProps, SizeModelTypeTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(new SizeModelTypeItemPropsToTableResolver()));



            CreateMap<SizeModelTypeTable, SizeModelType>()
           .ConstructUsing(src => SizeModelType.Load(
                   src.Id,
                   src.Code,
                   src.Name,
                   src.Status
               ));


            // Size Model Type Factors

            CreateMap<SizeModelTypeItemTable, SizeModelTypeItem>()
         .ConstructUsing(src => SizeModelTypeItem.Load(
                 src.Id,
                 src.Code,
                 src.Name,
                 null,
                 src.Status
             ));



            CreateMap<SizeModelTypeItemTable, SizeModelTypeItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SizeModelTypeId, opt => opt.MapFrom(src => src.SizeModelType.Id))
                .ForMember(dest => dest.SizeModelTypeName, opt => opt.MapFrom(src => src.SizeModelType.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).AfterMap<SizeModelTypeItemEnumAction>();


            CreateMap<SizeModelTypeItemTable, SizeModelTypeItemProps>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SizeModelType, opt => opt.MapFrom(src => src.SizeModelType))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
