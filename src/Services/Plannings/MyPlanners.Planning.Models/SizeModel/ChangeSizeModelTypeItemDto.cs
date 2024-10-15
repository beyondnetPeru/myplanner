namespace MyPlanner.Plannings.Models.SizeModel
{
    public class ChangeSizeModelTypeItemDto : AbstractUserDto
    {
        public string SizeModelTypeItemId { get; set; }
        public string SizeModelItemTypeCode { get; }

        public ChangeSizeModelTypeItemDto(string SizeModelTypeItemId, string sizeModelItemTypeCode, string userId) : base(userId)
        {
            this.SizeModelTypeItemId = SizeModelTypeItemId;
            SizeModelItemTypeCode = sizeModelItemTypeCode;
        }
    }
}
