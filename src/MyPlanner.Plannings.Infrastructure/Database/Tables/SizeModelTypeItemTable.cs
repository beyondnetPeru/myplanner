using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTypeItemTable
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("SizeModelType")]
        public string SizeModelTypeId { get; set; }
        public SizeModelTypeTable SizeModelType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
