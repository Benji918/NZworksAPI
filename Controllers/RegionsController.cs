using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZworks.Data;
using NZworks.Models.Domain;
using NZworks.Models.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace NZworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDBContext dbContext;
        public RegionsController(NzWalksDBContext dbContext)
        {
            this.dbContext = dbContext;

        }

        [HttpPatch]
        [SwaggerOperation(
            Summary = "Update a region",
            Description = "Update a region in the DB"
        )]
        public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
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


            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

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
            var test = await dbContext.Regions.FindAsync(id);

            Console.WriteLine(test.Name);

            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

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
        public IActionResult GetAllRegions()
        {

            var regions = dbContext.Regions.ToList();

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
