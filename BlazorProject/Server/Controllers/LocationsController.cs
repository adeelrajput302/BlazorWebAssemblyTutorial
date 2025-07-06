using BlazorProject.Server.Models;
using BlazorProject.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {

        private readonly ILocationRepository locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }



        [HttpGet]
        public async Task<ActionResult> GetLocations()
        {
            try
            {
                return Ok(await locationRepository.GetLocations());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Get BY ID

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            try
            {
                var result = await locationRepository.GetLocation(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }



        // Create for get dropdown list

        [HttpGet("GetLocationDto/{id:int}")]
        public async Task<ActionResult<LocationDto>> GetLocationDto(int id)
        {
            try
            {
                var getDto = await locationRepository.GetLocationDto(id);
                if (getDto == null)
                {
                    return NotFound();
                }
                var LocationDto = new LocationDto
                {
                    LocationId = getDto.LocationId,
                    LocationName = getDto.LocationName,
                };
                return Ok(LocationDto);
                
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }



        // Create Location 


        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(Location location)
        {
            try
            {

                if (location == null)
                    return BadRequest();

                var createdLocation = await locationRepository.AddLocation(location);

                return CreatedAtAction(nameof(GetLocation),
                    new { id = createdLocation.LocationId }, createdLocation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }


        }




        [HttpPut("{id:int}")]
        public async Task<ActionResult<Location>> UpdateLocation(int id, Location location)
        {
            try
            {
                if (id != location.LocationId)
                    return BadRequest("Location Id mismatch");

                var locationToUpdate = await locationRepository.GetLocation(id);

                if (locationToUpdate == null)
                {
                    return NotFound($"Location with Id = {id} not found");
                }

                return await locationRepository.UpdateLocation(location);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating employee record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            try
            {
                var locationToDelete = await locationRepository.GetLocation(id);

                if (locationToDelete == null)
                {
                    return NotFound($"Location with Id = {id} not found");
                }

                await locationRepository.DeleteLocation(id);

                return Ok($"Employee with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting employee record");
            }
        }

    }
}
