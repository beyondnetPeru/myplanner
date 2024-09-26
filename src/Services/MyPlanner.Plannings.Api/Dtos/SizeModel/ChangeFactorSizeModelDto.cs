namespace MyPlanner.Plannings.Api.Dtos.SizeModel
{
    public class ChangeFactorSizeModelDto : AbstractUserDto
    {
        public int FactorSelected { get; set; }

        public ChangeFactorSizeModelDto(int factorSelected, string userId) : base(userId)
        {
            FactorSelected = factorSelected;
        }
    }
}
