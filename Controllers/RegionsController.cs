using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZworks.Models.Domain;
using Swashbuckle.AspNetCore.Annotations;

namespace NZworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all regions",
            Description = "Get all regions in New Zealand"
        )]
        public IActionResult GetAllRegions()
        {
            List<Region> regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "AUK",
                    Name = "Auckland",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "WGN",
                    Name = "Wellington",
                    RegionImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb"
                },
            };
            return Ok(regions);
        }
    }
}
