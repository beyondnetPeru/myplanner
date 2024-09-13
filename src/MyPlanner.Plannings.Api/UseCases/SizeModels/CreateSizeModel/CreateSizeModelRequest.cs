using MediatR;
using MyPlanner.Plannings.Api.Dtos.SizeModel;

namespace MyPlanner.Plannings.Api.UseCases.SizeModels.CreateSizeModel
{
    public class CreateSizeModelRequest : IRequest<bool>
    {
        public string Code { get; set; }
        public string SizeModelTypeCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsStandard { get; set; }

        public string UserId { get; set; }
        public ICollection<SizeModelItemDto> SizeModelItems { get; set; }

        public CreateSizeModelRequest(string sizeModelTypeCode, string name, string description, int isStandard, string userId, ICollection<SizeModelItemDto> sizeModelItems)
        {
            SizeModelTypeCode = sizeModelTypeCode;
            Name = name;
            Description = description;
            IsStandard = isStandard;
            UserId = userId;
            SizeModelItems = sizeModelItems;
        }
    }

}