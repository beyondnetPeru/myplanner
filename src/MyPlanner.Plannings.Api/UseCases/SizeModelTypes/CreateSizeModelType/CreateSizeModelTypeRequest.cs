namespace MyPlanner.Plannings.Api.UseCases.SizeModelTypes.CreateSizeModelType
{
    public class CreateSizeModelTypeRequest : IRequest<bool>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public string UserId { get; private set; }

        public CreateSizeModelTypeRequest(string code, string name, string description, string userId)
        {
            Code = code;
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}
