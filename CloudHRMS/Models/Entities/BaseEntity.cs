using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.Models.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }//Primary Key for table created process
        //Audit Columns
        public DateTime CreatedAt { get; set; }//WHEN
        public string CreatedBy { get; set; }//WHO created this recrod
        public DateTime? UpdatedAt { get; set; }//WHEN Updated
        public string? UpdatedBy { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
