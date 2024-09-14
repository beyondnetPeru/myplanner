using AutoMapper;
using BeyondNet.Ddd.AutoMapper.Impl;
using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModelItem;
using MyPlanner.Plannings.Domain.SizeModels;
using MyPlanner.Plannings.Infrastructure.Database.Tables;

namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelProfile : Profile
    {
        public SizeModelProfile()
        {
            CreateMap<int, SizeModelStatus>().ConvertUsing<EnumerationConverter<SizeModelStatus>>();

            CreateMap<CreateSizeModelDto, CreateSizeModelRequest>();
            CreateMap<CreateSizeModelItemDto, CreateSizeModelItemRequest>();

            CreateMap<SizeModelDto, SizeModelProps>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.SizeModelItems, opt => opt.MapFrom(src => src.SizeModelItems))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<SizeModelProps, SizeModelDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
               .ForMember(dest => dest.SizeModelItems, opt => opt.MapFrom(src => src.SizeModelItems))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<CreateSizeModelRequest, SizeModelProps>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SizeModelItems, opt => opt.MapFrom(src => src.SizeModelItems));

            CreateMap<CreateSizeModelItemRequest, SizeModelItemProps>()
                //.ForMember(dest => dest.Profile.GetValue().ProfileName, opt => opt.MapFrom(src => src.ProfileName))
                //.ForMember(dest => dest.Profile.GetValue().ProfileAvgRate, opt => opt.MapFrom(src => src.ProfileAvgRateAmount))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProfileQuantity))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.SizeModelTypeFactor, opt => opt.MapFrom(src => src.SizeModelTypeFactorCode));

            CreateMap<SizeModelProps, SizeModelTable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.GetValue()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.GetValue()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Id));

            CreateMap<SizeModelTable, SizeModelProps>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<SizeModelItemProps, SizeModelItemTable>()
                .ForMember(dest => dest.ProfileName, opt => opt.MapFrom(src => src.Profile.GetValue().ProfileName))
                .ForMember(dest => dest.ProfileAvgRateAmount, opt => opt.MapFrom(src => src.Profile.GetValue().ProfileAvgRate))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost));


            CreateMap<SizeModelItemTable, SizeModelItemProps>()
                //.ForMember(dest => dest.Profile.GetValue().ProfileName, opt => opt.MapFrom(src => src.ProfileName))
                //.ForMember(dest => dest.Profile.GetValue().ProfileAvgRate, opt => opt.MapFrom(src => src.ProfileAvgRateAmount))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.TotalCost));
        }
    }
}
