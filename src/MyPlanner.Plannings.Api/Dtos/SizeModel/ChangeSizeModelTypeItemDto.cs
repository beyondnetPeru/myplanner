namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ChangeSizeModelTypeItemDto : AbstractUserDto
    {
        public string SizeModelTypeItemId { get; set; }

        public ChangeSizeModelTypeItemDto(string SizeModelTypeItemId, string userId) : base(userId)
        {
            this.SizeModelTypeItemId = SizeModelTypeItemId;
        }
    }
}
