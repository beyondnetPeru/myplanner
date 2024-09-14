namespace MyPlanner.Plannings.Api.UseCases.SizeModels.Command.CreateSizeModel
{
    public class CreateSizeModelRequest : IRequest<bool>
    {
        public string Code { get; set; }
        public string SizeModelTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IsStandard { get; set; }
        public string UserId { get; set; }

        public CreateSizeModelRequest(string sizeModelTypeId, string code, string name, string description, int isStandard, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Code = code;
            Name = name;
            Description = description;
            IsStandard = isStandard;
            UserId = userId;
        }
    }

}