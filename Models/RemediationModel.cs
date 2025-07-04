using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vulnerabilty_Project.Models
{
    public class RemediationModel
    {
        [Key]
        public int RemediationID { get; set; }

        [Required(ErrorMessage = "Vulnerability ID is required.")]
        [ForeignKey("Vulnerability")]
        public int VulnID { get; set; }

        [Required(ErrorMessage = "Engineer ID is required.")]
        [ForeignKey("Engineer")]
        public int EngineerID { get; set; }

        [Required(ErrorMessage = "Action Taken is required.")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "Action Taken cannot exceed 1000 characters.")]
        public string ActionTaken { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }

        public virtual VulnerbilitiesModel Vulnerability { get; set; }
        public virtual UserModel Engineer { get; set; }
    }
}