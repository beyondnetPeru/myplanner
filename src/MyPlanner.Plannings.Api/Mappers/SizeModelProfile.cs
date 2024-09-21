using MyPlanner.Plannings.Api.Dtos.SizeModel;
using MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel;


namespace MyPlanner.Plannings.Api.Mappers
{
    public class SizeModelProfile : Profile
    {
        public SizeModelProfile()
        {
            // SizeModel
            CreateMap<CreateSizeModelDto, CreateSizeModelRequest>();

            //SizeModelItem
            CreateMap<CreateSizeModelItemDto, CreateSizeModelItemRequest>();
        }
    }
}
