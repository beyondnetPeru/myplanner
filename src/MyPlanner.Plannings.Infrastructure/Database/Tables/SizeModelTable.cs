using MyPlanner.Plannings.Domain.SizeModelTypes;
using MyPlanner.Plannings.Shared.Infrastructure.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTable
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("SizeModelTypeId")]
        public string SizeModelTypeId { get; set; }
        public string SizeModelTypeCode { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelItemTable> Items { get; set; }
        public AuditTable Audit { get; set; }
        public int Status { get; set; }
    }

}
