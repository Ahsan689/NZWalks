using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepository, IMapper mapper)
        {
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        // CREATE WALK
        // POST : /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            // Map DTO to Domain Model
            var walksDomainModel = mapper.Map<Walks>(addWalksRequestDto);
            walksDomainModel = await walksRepository.CreateAsync(walksDomainModel);

            // Map Domain model to DTO

            return Ok(mapper.Map<WalkDto>(walksDomainModel));
         
        }

        // GET WALK
        // GET : /api/walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walksDomainModel = await walksRepository.GetAllAsync(filterOn, filterQuery);

            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {

             var walksDomainModel = mapper.Map<Walks>(updateWalksRequestDto);

            walksDomainModel = await walksRepository.UpdateAsync(id, walksDomainModel);

            var walksDto = mapper.Map<WalkDto>(walksDomainModel);
            return Ok(walksDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walksRepository.GetByIdAsync(id);

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var walkDomainModel = await walksRepository.DeleteAsync(id);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
