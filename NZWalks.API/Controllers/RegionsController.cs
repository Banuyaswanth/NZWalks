using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateRegion(CreateRegionRequestDTO createRegionRequestDTO)
        {
            var isCodeValid = await regionRepository.GetRegionByCodeAsync(createRegionRequestDTO.Code);
            if (isCodeValid == null)
            {
                var regionDomainModel = mapper.Map<Region>(createRegionRequestDTO);
                var newRegionDomain = await regionRepository.CreateRegionAsync(regionDomainModel);
                var newRegionDTO = mapper.Map<RegionDetailsDTO>(newRegionDomain);
                return Ok(newRegionDTO);
            }
            return BadRequest("Region with the given code already exists");
        }

        [HttpGet]
        [Authorize(Roles ="Reader,Writer")]
        public async Task<IActionResult> GetRegions()
        {
            var regionsList = await regionRepository.GetAllRegionsAsync();
            if(regionsList.Count == 0)
            {
                return Ok("No regions to display");
            }
            var regionsDTOList = mapper.Map<List<RegionDetailsDTO>>(regionsList);
            return Ok(regionsDTOList);
        }
    }
}
