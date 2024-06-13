using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            logger.LogInformation(" Regions GetAll Method Invoked....");
            // Get Data from Database - Domain models;
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTO
            //var regionsDto = new List<RegionDto>();

            //foreach (var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageUrl,
            //    }
            //    );
            //};
            
            // return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));

        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // This is for Finding with unique id only
            // var regions = dbContext.Regions.Find(id);

            // Get Region Domain Model from Database

            // This is for Finding with any property..
            var regionsDomain = await regionRepository.GetByIdAsync(id);


            if (regionsDomain == null)
            {
                return NotFound();
            }

            // Map Region Domain Model to DTO 

           
            // Return DTO back to Client...
            return Ok(mapper.Map<RegionDto>(regionsDomain));

        }

        // POST to Create New Region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {


            // Map or Convert DTO to Domain Model

            var regionModel = mapper.Map<Region>(addRegionRequestDto);

            regionModel =  await regionRepository.CreateAsync(regionModel);

            // Map domain model back to DTO.....
           var regionDto =  mapper.Map<RegionDto>(regionModel);

            //var RegionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //};

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
          

        }

        // UPDATE Region......
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map domain model back to DTO.....
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
         

        }


        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // check if region exist....
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }


            // Map domain model back to DTO.....
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
    }

    
}
