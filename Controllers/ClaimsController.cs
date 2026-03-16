using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PolicyClaimAPI.Data;
using PolicyClaimAPI.Models;
using PolicyClaimAPI.DTOs;

namespace PolicyClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(
            ApplicationDbContext context, 
            IMapper mapper,
            ILogger<ClaimsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimResponseDto>>> GetClaims(
            [FromQuery] string? status = null,
            [FromQuery] string? claimType = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var query = _context.Claims
                    .Include(c => c.Documents)
                    .Include(c => c.Notes)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(status))
                    query = query.Where(c => c.Status == status);

                if (!string.IsNullOrWhiteSpace(claimType))
                    query = query.Where(c => c.ClaimType == claimType);

                var totalCount = await query.CountAsync();
                var claims = await query
                    .OrderByDescending(c => c.DateFiled)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var claimDtos = _mapper.Map<List<ClaimResponseDto>>(claims);

                Response.Headers.Append("X-Total-Count", totalCount.ToString());
                Response.Headers.Append("X-Total-Pages", Math.Ceiling(totalCount / (double)pageSize).ToString());

                return Ok(claimDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claims");
                return StatusCode(500, "An error occurred while retrieving claims");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Claim>> GetClaim(int id)
        {
            try
            {
                var claim = await _context.Claims
                    .Include(c => c.Documents)
                    .Include(c => c.Notes)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (claim == null)
                {
                    return NotFound($"Claim with ID {id} not found");
                }

                return Ok(claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claim {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the claim");
            }
        }

        [HttpGet("number/{claimNumber}")]
        public async Task<ActionResult<Claim>> GetClaimByNumber(string claimNumber)
        {
            try
            {
                var claim = await _context.Claims
                    .Include(c => c.Documents)
                    .Include(c => c.Notes)
                    .FirstOrDefaultAsync(c => c.ClaimNumber == claimNumber);

                if (claim == null)
                {
                    return NotFound($"Claim with number {claimNumber} not found");
                }

                return Ok(claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claim {ClaimNumber}", claimNumber);
                return StatusCode(500, "An error occurred while retrieving the claim");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Claim>> CreateClaim(CreateClaimDto createDto)
        {
            try
            {
                var claim = _mapper.Map<Claim>(createDto);
                
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                var initialNote = new ClaimNote
                {
                    ClaimId = claim.Id,
                    Note = "Claim submitted successfully",
                    CreatedBy = "System"
                };
                
                _context.ClaimNotes.Add(initialNote);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetClaim), new { id = claim.Id }, claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating claim");
                return StatusCode(500, "An error occurred while creating the claim");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaim(int id, UpdateClaimDto updateDto)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null)
                {
                    return NotFound($"Claim with ID {id} not found");
                }

                if (!string.IsNullOrWhiteSpace(updateDto.ClaimantName))
                    claim.ClaimantName = updateDto.ClaimantName;
                
                if (!string.IsNullOrWhiteSpace(updateDto.ClaimantEmail))
                    claim.ClaimantEmail = updateDto.ClaimantEmail;
                
                if (!string.IsNullOrWhiteSpace(updateDto.ClaimantPhone))
                    claim.ClaimantPhone = updateDto.ClaimantPhone;
                
                if (updateDto.ClaimAmount.HasValue)
                    claim.ClaimAmount = updateDto.ClaimAmount.Value;
                
                if (!string.IsNullOrWhiteSpace(updateDto.Description))
                    claim.Description = updateDto.Description;
                
                if (!string.IsNullOrWhiteSpace(updateDto.Status))
                {
                    if (ClaimStatus.AllStatuses.Contains(updateDto.Status))
                        claim.Status = updateDto.Status;
                    else
                        return BadRequest($"Invalid status. Allowed values: {string.Join(", ", ClaimStatus.AllStatuses)}");
                }
                
                if (!string.IsNullOrWhiteSpace(updateDto.AssignedAdjuster))
                    claim.AssignedAdjuster = updateDto.AssignedAdjuster;

                claim.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating claim {Id}", id);
                return StatusCode(500, "An error occurred while updating the claim");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateClaimStatus(int id, [FromBody] string status)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null)
                {
                    return NotFound($"Claim with ID {id} not found");
                }

                if (!ClaimStatus.AllStatuses.Contains(status))
                {
                    return BadRequest($"Invalid status. Allowed values: {string.Join(", ", ClaimStatus.AllStatuses)}");
                }

                claim.Status = status;
                claim.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Claim status updated to '{status}'" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating claim status {Id}", id);
                return StatusCode(500, "An error occurred while updating claim status");
            }
        }

        [HttpPost("{id}/notes")]
        public async Task<IActionResult> AddNote(int id, AddNoteDto noteDto)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null)
                {
                    return NotFound($"Claim with ID {id} not found");
                }

                var note = new ClaimNote
                {
                    ClaimId = id,
                    Note = noteDto.Note,
                    CreatedBy = noteDto.CreatedBy
                };

                _context.ClaimNotes.Add(note);
                await _context.SaveChangesAsync();

                return Ok(note);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding note to claim {Id}", id);
                return StatusCode(500, "An error occurred while adding the note");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            try
            {
                var claim = await _context.Claims.FindAsync(id);
                if (claim == null)
                {
                    return NotFound($"Claim with ID {id} not found");
                }

                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();

                return Ok(new { message = $"Claim {id} deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting claim {Id}", id);
                return StatusCode(500, "An error occurred while deleting the claim");
            }
        }

        [HttpGet("stats/summary")]
        public async Task<ActionResult> GetClaimStats()
        {
            try
            {
                var stats = new
                {
                    TotalClaims = await _context.Claims.CountAsync(),
                    ByStatus = await _context.Claims
                        .GroupBy(c => c.Status)
                        .Select(g => new { Status = g.Key, Count = g.Count() })
                        .ToListAsync(),
                    ByType = await _context.Claims
                        .GroupBy(c => c.ClaimType)
                        .Select(g => new { Type = g.Key, Count = g.Count() })
                        .ToListAsync(),
                    TotalClaimAmount = await _context.Claims.SumAsync(c => c.ClaimAmount),
                    AverageClaimAmount = await _context.Claims.AverageAsync(c => c.ClaimAmount)
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claim stats");
                return StatusCode(500, "An error occurred while retrieving claim statistics");
            }
        }
    }
}