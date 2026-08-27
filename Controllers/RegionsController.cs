using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZworks.Data;
using NZworks.Models.Domain;
using NZworks.Models.DTO;
using NZworks.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace NZworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDBContext dbContext;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(NzWalksDBContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            _regionRepository = regionRepository;

        }

        [HttpDelete]
        [SwaggerOperation(
            Summary = "Delete a region",
            Description = "Delete a region in the DB"
        )]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {

            // Check if region exists
            var region = await dbContext.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //Delete the region
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();


            return NoContent();
        }

        [HttpPatch]
        [SwaggerOperation(
            Summary = "Update a region",
            Description = "Update a region in the DB"
        )]
        public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Check if region exists
            var region = await dbContext.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            // Update the region properties
            region.Name = updateRegionRequestDTO.Name;
            region.Code = updateRegionRequestDTO.Code;
            region.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Map domain model back to DTO
            RegionDTO regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a region",
            Description = "Create a region in the DB"
        )]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to domain model
            Region region = new Region
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };


            var region_rep = _regionRepository.CreateAsync(region);

            // Map doman model back to DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(
                actionName: nameof(GetRegionById),
                routeValues: new { id = regionDTO.Id },
                value: regionDTO);
        }

        [HttpGet("{id:Guid}")]
        [SwaggerOperation(
            Summary = "Get region by ID",
            Description = "Get speciic region by ID"
        )]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            //var test = await dbContext.Regions.FindAsync(id);

            //Console.WriteLine(test.Name);

            var region = await _regionRepository.GetById(id: id);

            if (region == null)
            {
                return NotFound();
            }

            //Map the DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all regions",
            Description = "Get all regions in New Zealand"
        )]
        public async Task<IActionResult> GetAllRegions()
        {

            var regions = await _regionRepository.GetAllAsync();

            //Map Domain models to DTO
            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDTO.Add(
                        new RegionDTO
                        {
                            Id = region.Id,
                            Code = region.Code,
                            Name = region.Name,
                            RegionImageUrl = region.RegionImageUrl
                        }
                    );
            }


            return Ok(regionsDTO);


        }
    }
}
