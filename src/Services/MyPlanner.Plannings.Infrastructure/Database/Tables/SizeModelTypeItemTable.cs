namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTypeItemTable
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("SizeModelTypeId")]
        public string SizeModelTypeId { get; set; }
        public SizeModelTypeTable SizeModelType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
