using Microsoft.EntityFrameworkCore;
using PolicyClaimAPI.Models;

namespace PolicyClaimAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimDocument> ClaimDocuments { get; set; }
        public DbSet<ClaimNote> ClaimNotes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Claim>()
                .Property(c => c.ClaimAmount)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Claim>()
                .HasMany(c => c.Documents)
                .WithOne(d => d.Claim)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Claim>()
                .HasMany(c => c.Notes)
                .WithOne(n => n.Claim)
                .HasForeignKey(n => n.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Claim>().HasData(
                new Claim
                {
                    Id = 1,
                    ClaimNumber = "CLM-2025-001",
                    PolicyNumber = "POL-AUTO-12345",
                    ClaimantName = "John Doe",
                    ClaimantEmail = "john.doe@email.com",
                    DateOfLoss = new DateTime(2025, 1, 15),
                    ClaimType = "Auto",
                    ClaimAmount = 5000.00m,
                    Description = "Rear-end collision at traffic light",
                    Status = ClaimStatus.Approved,
                    AssignedAdjuster = "Sarah Johnson"
                },
                new Claim
                {
                    Id = 2,
                    ClaimNumber = "CLM-2025-002",
                    PolicyNumber = "POL-HOME-67890",
                    ClaimantName = "Jane Smith",
                    ClaimantEmail = "jane.smith@email.com",
                    DateOfLoss = new DateTime(2025, 2, 3),
                    ClaimType = "Home",
                    ClaimAmount = 12500.00m,
                    Description = "Water damage from burst pipe",
                    Status = ClaimStatus.UnderReview,
                    AssignedAdjuster = "Mike Wilson"
                }
            );
        }
    }
}