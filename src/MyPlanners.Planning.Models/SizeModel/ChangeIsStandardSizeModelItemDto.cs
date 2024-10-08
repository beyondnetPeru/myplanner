namespace MyPlanner.Plannings.Models.SizeModel
{
    public class ChangeIsStandardSizeModelItemDto : AbstractUserDto
    {
        public bool IsStandard { get; set; }
        
        public ChangeIsStandardSizeModelItemDto(bool isStandard, string userId) : base(userId)
        {
            IsStandard = isStandard;
        }
    }
}
