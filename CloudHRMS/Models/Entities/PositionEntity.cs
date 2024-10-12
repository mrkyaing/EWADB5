using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Position")]
    public class PositionEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
    }
}