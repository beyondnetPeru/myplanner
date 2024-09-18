using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ActivateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.AddSizeModelTypeFactor;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeCodeSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.ChangeNameSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.CreateSizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.Commands.DeactivateSizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;
using MyPlanner.Plannings.Shared.Domain.ValueObjects;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelTypeFactorResolver : IValueResolver<CreateSizeModelTypeRequest, SizeModelTypeProps, ICollection<SizeModelTypeFactor>>
    {
        public ICollection<SizeModelTypeFactor> Resolve(CreateSizeModelTypeRequest source, SizeModelTypeProps destination, ICollection<SizeModelTypeFactor> destMember, ResolutionContext context)
        {
            var factors = new List<SizeModelTypeFactor>();

            source.Factors.ToList().ForEach(item =>
            {
                var factor = SizeModelTypeFactor.Create(IdValueObject.Create(),
                                                        SizeModelTypeFactorCode.Create(item.Code),
                                                        Name.Create(item.Name),
                                                        null);

                factors.Add(factor);
            });

            return factors;
        }
    }

    public class SizeModelTypeEnumAction : IMappingAction<SizeModelTypeTable, SizeModelTypeDto>
    {
        public void Process(SizeModelTypeTable source, SizeModelTypeDto destination, ResolutionContext context)
        {
            destination.Status = Enumeration.FromValue<SizeModelTypeStatus>(source.Status).Name.ToUpper();
        }
    }

    public class SizeModelTypeFcatorEnumAction : IMappingAction<SizeModelTypeFactorTable, SizeModelTypeFactorDto>
    {
        public void Process(SizeModelTypeFactorTable source, SizeModelTypeFactorDto destination, ResolutionContext context)
        {
            destination.Status = Enumeration.FromValue<SizeModelTypeFactorStatus>(source.Status).Name.ToUpper();
        }
    }

    public class SizeModelTypeProfile : Profile
    {
        public SizeModelTypeProfile()
        {
            // Size Model Types
            CreateMap<int, SizeModelTypeStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeStatus>>();
            CreateMap<int, SizeModelTypeFactorStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeFactorStatus>>();

            CreateMap<ChangeNameSizeModelTypeDto, ChangeNameSizeModelTypeRequest>();
            CreateMap<ActivateSizeModelTypeDto, ActivateSizeModelTypeRequest>();
            CreateMap<DeactivateSizeModelTypeDto, DeactivateSizeModelTypeRequest>();
            CreateMap<CreateSizeModelTypeDto, CreateSizeModelTypeRequest>();
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
                ForMember(dest => dest.Factors, opt => opt.MapFrom(new SizeModelTypeFactorResolver()));




            CreateMap<SizeModelTypeFactorProps, SizeModelTypeFactorTable>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
            .ForMember(dest => dest.SizeModelType, opt => opt.MapFrom(src => src.SizeModelType.GetPropsCopy()))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));


            CreateMap<SizeModelTypeProps, SizeModelTypeTable>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id))
            .ForMember(dest => dest.Factors, opt => opt.MapFrom(src => src.Factors));


            CreateMap<SizeModelTypeTable, SizeModelType>()
                .ConstructUsing(src => SizeModelType.Load(
                        src.Id,
                        src.Code,
                        src.Name,
                        src.Status
                    ));

            // Size Model Type Factors
            CreateMap<AddSizeModelTypeFactorDto, AddSizeModelTypeFactorRequest>();

            CreateMap<SizeModelTypeFactorTable, SizeModelTypeFactorProps>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SizeModelType, opt => opt.MapFrom(src => src.SizeModelType))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));



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
