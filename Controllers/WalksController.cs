using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZworks.Data;
using NZworks.Models.Domain;
using NZworks.Models.DTO;
using NZworks.Repositories;

namespace NZworks.Controllers
{
    // api/works
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NzWalksDBContext _dbcontext;
        private readonly IWalkRepository _walkRepository;

        public WalksController(NzWalksDBContext dbcontext, IWalkRepository walkRepository)
        {
            _dbcontext = dbcontext;
            _walkRepository = walkRepository;
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
            walk = await _walkRepository.AddWalk(walk);

            return CreatedAtAction(nameof(GetWalkById), new { id = walk.Id }, walk);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await _walkRepository.GetWalkById(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(walk);
        }


    }
}
