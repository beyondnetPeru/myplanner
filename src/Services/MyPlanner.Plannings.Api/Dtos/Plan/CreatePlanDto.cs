namespace MyPlanner.Plannings.Api.Dtos.Plan
{
    public class CreatePlanDto
    {
        public string Name { get; set; } = default!;
        public ICollection<PlanCategoryDto> Categories { get; set; } = default!;
        public string Owner { get; set; } = default!;
        public string SizeModelTypeId { get; set; } = default!;
        public ICollection<CreatePlanItemDto> Items { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
