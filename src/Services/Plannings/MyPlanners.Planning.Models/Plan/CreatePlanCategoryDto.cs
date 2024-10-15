namespace MyPlanner.Plannings.Models.Plan
{
    public class CreatePlanCategoryDto : AbstractUserDto { 
        public string Name { get; set; } = default!;

        public CreatePlanCategoryDto(string userId) : base(userId)
        {
            
        }
    }
}
