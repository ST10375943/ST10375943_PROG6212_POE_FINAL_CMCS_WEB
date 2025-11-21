using System;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Web.Models
{
    public class Claim
    {
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; } = string.Empty;

        [Required]
        public string LecturerEmail { get; set; } = string.Empty;

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public decimal TotalAmount => HoursWorked * HourlyRate;

        // --- NEW FIELDS YOU WERE MISSING ---
        public string? CoordinatorComments { get; set; }
        public string? HRComments { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
    }

    public enum ClaimStatus
    {
        Pending = 0,
        ApprovedByCoordinator = 1,
        RejectedByCoordinator = 2,
        ProcessedByHR = 3,
        RejectedByHR = 4
    }
}
