using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZworks.Data;
using NZworks.Models.Domain;
using NZworks.Models.DTO;

namespace NZworks.Controllers
{
    // api/works
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NzWalksDBContext _dbcontext;

        public WalksController(NzWalksDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]

        public async Task<IActionResult> AddWalk([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            if (addWalkRequestDTO == null)
            {
                return BadRequest("Walk object is null");
            }
            // Map the DTO to the domain model
            var walk = new Walk
            {
                Name = addWalkRequestDTO.Name,
                Description = addWalkRequestDTO.Description,
                LengthInKm = addWalkRequestDTO.LengthInKm,
                WalkImageUrl = addWalkRequestDTO.WalkImageUrl,
                RegionId = addWalkRequestDTO.RegionId,
                DifficultyId = addWalkRequestDTO.DifficultyId
            };
            // Add the walk to the database
            await _dbcontext.Walks.AddAsync(walk);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walk);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await _dbcontext.Walks.FindAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(walk);
        }


    }
}
