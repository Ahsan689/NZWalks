using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from Database - Domain models;
            var regionsDomain = _dbContext.Regions.ToList();

            // Map Domain Models to DTO
            var regionsDto = new List<RegionDto>();

            foreach(var region in regionsDomain) 
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                }
                );
            };
            // return DTOs
            return Ok(regionsDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // This is for Finding with unique id only
            // var regions = dbContext.Regions.Find(id);

            // Get Region Domain Model from Database

            // This is for Finding with any property..
            var regionsDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);


            if (regionsDomain == null)
            {
                return NotFound();
            }

            // Map Region Domain Model to DTO 

            var regionsDto = new RegionDto
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl,
            };
            // Return DTO back to Client...
            return Ok(regionsDto);

        }
    }
}
