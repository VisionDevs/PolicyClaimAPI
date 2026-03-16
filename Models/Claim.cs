using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PolicyClaimAPI.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ClaimNumber { get; set; } = string.Empty;
        
        [Required]
        public string PolicyNumber { get; set; } = string.Empty;
        
        [Required]
        public string ClaimantName { get; set; } = string.Empty;
        
        [Required]
        public string ClaimantEmail { get; set; } = string.Empty;
        
        public string ClaimantPhone { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfLoss { get; set; }
        
        [Required]
        public DateTime DateFiled { get; set; } = DateTime.UtcNow;
        
        [Required]
        public string ClaimType { get; set; } = string.Empty;
        
        [Required]
        public decimal ClaimAmount { get; set; }
        
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Status { get; set; } = "Submitted";
        
        public string AssignedAdjuster { get; set; } = string.Empty;
        
        public DateTime? LastUpdated { get; set; }
        
        public List<ClaimDocument> Documents { get; set; } = new();
        public List<ClaimNote> Notes { get; set; } = new();
    }
    
    public class ClaimDocument
    {
        [Key]
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Claim? Claim { get; set; }
    }
    
    public class ClaimNote
    {
        [Key]
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public string Note { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Claim? Claim { get; set; }
    }
}