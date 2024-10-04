namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class CreatePlanDto : AbstractUserDto
    {
        public CreatePlanDto(string userId) : base(userId)
        {
        }

        public string Name { get; set; } = default!;
        public ICollection<CreatePlanCategoryDto> Categories { get; set; } = default!;
        public string Owner { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public ICollection<CreatePlanItemDto> Items { get; set; } = default!;
    }
}
