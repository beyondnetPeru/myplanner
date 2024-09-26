namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ChangeCodeSizeModelTypeItemDto : AbstractUserDto
    {
        public string Code { get; set; }

        public ChangeCodeSizeModelTypeItemDto(string code, string userId) : base(userId)
        {
            Code = code;
        }
    }
}
