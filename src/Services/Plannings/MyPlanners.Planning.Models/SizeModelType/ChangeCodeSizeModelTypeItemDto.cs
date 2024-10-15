namespace MyPlanner.Plannings.Models.SizeModelType
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
