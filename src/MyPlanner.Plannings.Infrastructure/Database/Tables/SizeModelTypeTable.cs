
namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTypeTable
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelTypeFactorTable> Factors { get; set; }
        public int Status { get; set; }
    }
}
