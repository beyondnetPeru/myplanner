using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelTypeEnumAction : IMappingAction<SizeModelTypeTable, SizeModelTypeDto>
    {
        public void Process(SizeModelTypeTable source, SizeModelTypeDto destination, ResolutionContext context)
        {
            destination.Status = Enumeration.FromValue<SizeModelTypeStatus>(source.Status).Name.ToUpper();
        }
    }

    public class SizeModelTypeProfile : Profile
    {
        public SizeModelTypeProfile()
        {
            CreateMap<int, SizeModelTypeStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeStatus>>();

            CreateMap<CreateSizeModelTypeDto, CreateSizeModelTypeRequest>();

            CreateMap<SizeModelTypeTable, SizeModelTypeDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)).AfterMap<SizeModelTypeEnumAction>();

            CreateMap<CreateSizeModelTypeRequest, SizeModelTypeProps>()
                .ConstructUsing(src => new SizeModelTypeProps(IdValueObject.DefaultValue, SizeModelTypeCode.Create(src.Code), Name.Create(src.Name)));

            CreateMap<SizeModelTypeProps, SizeModelTypeTable>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
              .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<ChangeCodeSizeModelTypeDto, ChangeCodeSizeModelTypeRequest>();

            CreateMap<SizeModelTypeTable, SizeModelType>()
                .ConstructUsing(src => SizeModelType.Load(
                        src.Id,
                        src.Code,
                        src.Name,
                        src.Status
                    ));

            CreateMap<ChangeNameSizeModelTypeDto, ChangeNameSizeModelTypeRequest>();

            CreateMap<ActivateSizeModelTypeDto, ActivateSizeModelTypeRequest>();

            CreateMap<DeactivateSizeModelTypeDto, DeactivateSizeModelTypeRequest>();

            //CreateMap<SizeModelTypeFactorTable, SizeModelTypeFactorProps>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            //CreateMap<SizeModelType, SizeModelTypeDto>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetProps().Id.GetValue()))
            //    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.GetProps().Code.GetValue()))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetProps().Name.GetValue()));


            //CreateMap<SizeModelTypeFactor, SizeModelTypeFactorDto>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetProps().Id.GetValue()))
            //   .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.GetProps().Code.GetValue()))
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetProps().Name.GetValue()));

            //CreateMap<CreateSizeModelTypeDto, CreateSizeModelTypeRequest>();


            //CreateMap<SizeModelTypeTable, SizeModelTypeProps>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            //    .ForMember(dest => dest.Factors, opt => opt.Ignore());


            //CreateMap<SizeModelTypeFactorProps, SizeModelTypeFactorTable>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
            //    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()));


        }
    }
}
