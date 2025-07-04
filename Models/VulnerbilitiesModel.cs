using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vulnerabilty_Project.Models
{
    public enum Severity
    {
        [Display(Name = "Low Risk")]
        Low,
        [Display(Name = "Medium Risk")]
        Medium,
        [Display(Name = "High Risk")]
        High,
        [Display(Name = "Critical Risk")]
        Critical
    }
    public enum Status { Open, InProgress, Resolved, Closed }

    public class VulnerbilitiesModel
    {
        [Key]
        public int VulnID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity is required.")]
        [EnumDataType(typeof(Severity))]
        public Severity? SeverityLevel { get; set; } 

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(Status))]
        public Status? CurrentStatus { get; set; } 

        [Required(ErrorMessage = "Discovered date is required.")]
        [DataType(DataType.Date)]
        public DateTime DiscoveredDate { get; set; }

        [Required(ErrorMessage = "Affected system is required.")]
        [StringLength(100, ErrorMessage = "Affected system cannot exceed 100 characters.")]
        public string AffectedSystem { get; set; }

        [Required(ErrorMessage = "Reporter ID is required.")]
        public int ReporterID { get; set; }

        [ForeignKey("ReporterID")]
        public virtual UserModel Reporter { get; set; }

        public virtual ICollection<RemediationModel> Remediations { get; set; }
    }
}
