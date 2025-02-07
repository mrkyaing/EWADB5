﻿using CloudHRMS.Utility;
using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.Models.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }//Primary Key for table created process
        //Audit Columns
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }//WHO created this record
        public DateTime? UpdatedAt { get; set; }//WHEN Updated
        public string? UpdatedBy { get; set; }
        public string IpAddress { get; set; } = NetworkHelper.GetMachinePublicIP();
        public bool IsActive { get; set; } = true;

    }
}
