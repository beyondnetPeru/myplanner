namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class CreatePlanCategoryDto : AbstractUserDto
    {
        public CreatePlanCategoryDto(string userId) : base(userId)
        {
        }

        public string Name { get; set; } = default!;
    }
}
