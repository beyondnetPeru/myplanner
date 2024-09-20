namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTypeItemTable
    {
        public string Id { get; set; }
        public string SizeModelTypeId { get; set; }
        public SizeModelTypeTable SizeModelType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
