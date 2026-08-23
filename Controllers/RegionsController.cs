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

        [HttpGet("{Id}")]
        [SwaggerOperation(
            Summary = "Get region by ID",
            Description = "Get speciic region by ID"
        )]
        public async Task<IActionResult> GetRegionById(Guid Id)
        {
            var test = await dbContext.Regions.FindAsync(Id);

            Console.WriteLine(test);

            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == Id);

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
