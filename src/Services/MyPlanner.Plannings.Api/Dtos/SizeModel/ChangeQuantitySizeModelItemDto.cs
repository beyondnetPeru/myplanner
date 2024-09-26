namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ChangeQuantitySizeModelItemDto : AbstractUserDto
    {
        public ChangeQuantitySizeModelItemDto(int quantity, string userId) : base(userId)
        {
            Quantity = quantity;
        }

        public int Quantity { get; }
    }
}
