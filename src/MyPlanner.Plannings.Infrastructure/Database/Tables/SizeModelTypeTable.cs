
using System.ComponentModel.DataAnnotations;

namespace MyPlanner.Plannings.Infrastructure.Database.Tables
{
    public class SizeModelTypeTable
    {
        [Key]
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<SizeModelTypeItemTable> Items { get; set; }
        public int Status { get; set; }
    }
}
