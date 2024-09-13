namespace MyPlanner.Plannings.Api.Dtos.SizeModelType
{
    public class SizeModelTypeDto
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelTypeFactorDto> Factors { get; set; }
    }
}
