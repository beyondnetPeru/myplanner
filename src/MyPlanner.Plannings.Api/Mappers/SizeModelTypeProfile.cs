using AutoMapper;
using BeyondNet.Ddd.AutoMapper.Impl;
using MyPlanner.Plannings.Api.Dtos.SizeModelType;
using MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelTypeProfile : Profile
    {
        public SizeModelTypeProfile()
        {
            CreateMap<int, SizeModelTypeStatus>().ConvertUsing<EnumerationConverter<SizeModelTypeStatus>>();

            CreateMap<SizeModelType, SizeModelTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetProps().Id.GetValue()))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.GetProps().Code.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetProps().Name.GetValue()));


            CreateMap<SizeModelTypeTable, SizeModelTypeProps>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<SizeModelTypeProps, SizeModelTypeTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<CreateSizeModelTypeDto, CreateSizeModelTypeRequest>();

            CreateMap<SizeModelTypeFactor, SizeModelTypeFactorDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GetProps().Id.GetValue()))
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.GetProps().Code.GetValue()))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetProps().Name.GetValue()));

            CreateMap<SizeModelTypeFactorTable, SizeModelTypeFactorProps>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<SizeModelTypeFactorProps, SizeModelTypeFactorTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()));


        }
    }
}
