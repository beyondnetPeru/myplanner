namespace MyPlanner.Plannings.Models.SizeModelType
{
    public class ChangeCodeSizeModelTypeDto : AbstractUserDto
    {
        public ChangeCodeSizeModelTypeDto(string code, string userId) : base(userId)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
