using System.ComponentModel.DataAnnotations;

namespace PolicyClaimAPI.DTOs
{
    public class CreateClaimDto
    {
        [Required]
        public string PolicyNumber { get; set; } = string.Empty;
        
        [Required]
        public string ClaimantName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string ClaimantEmail { get; set; } = string.Empty;
        
        public string ClaimantPhone { get; set; } = string.Empty;
        
        [Required]
        public DateTime DateOfLoss { get; set; }
        
        [Required]
        public string ClaimType { get; set; } = string.Empty;
        
        [Required]
        public decimal ClaimAmount { get; set; }
        
        public string Description { get; set; } = string.Empty;
    }
    
    public class UpdateClaimDto
    {
        public string ClaimantName { get; set; } = string.Empty;
        public string ClaimantEmail { get; set; } = string.Empty;
        public string ClaimantPhone { get; set; } = string.Empty;
        public decimal? ClaimAmount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string AssignedAdjuster { get; set; } = string.Empty;
    }
    
    public class AddNoteDto
    {
        [Required]
        public string Note { get; set; } = string.Empty;
        
        [Required]
        public string CreatedBy { get; set; } = string.Empty;
    }
    
    public class ClaimResponseDto
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
        public string ClaimantName { get; set; } = string.Empty;
        public string ClaimantEmail { get; set; } = string.Empty;
        public string ClaimantPhone { get; set; } = string.Empty;
        public DateTime DateOfLoss { get; set; }
        public DateTime DateFiled { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string AssignedAdjuster { get; set; } = string.Empty;
        public DateTime? LastUpdated { get; set; }
        public int DocumentCount { get; set; }
        public int NoteCount { get; set; }
    }
}