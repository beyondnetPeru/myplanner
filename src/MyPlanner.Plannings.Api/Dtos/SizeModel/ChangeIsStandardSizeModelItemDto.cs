namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ChangeIsStandardSizeModelItemDto : AbstractUserDto
    {
        public bool IsStandard { get; set; }
        public string UserId { get; set; }

        public ChangeIsStandardSizeModelItemDto(bool isStandard, string userId) : base(userId)
        {
            IsStandard = isStandard;
            UserId = userId;
        }
    }
}
