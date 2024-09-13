namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class ChangeCodeSizeModelTypeDto
    {
        public ChangeCodeSizeModelTypeDto(string sizeModelTypeId, string code, string userId)
        {
            SizeModelTypeId = sizeModelTypeId;
            Code = code;
            UserId = userId;
        }

        public string SizeModelTypeId { get; }
        public string Code { get; }
        public string UserId { get; set; }
    }
}
