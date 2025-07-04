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

        

    }
}
