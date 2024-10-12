using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Department")]
    public class DepartmentEntity : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string ExtensionPhone { get; set; }

    }
}