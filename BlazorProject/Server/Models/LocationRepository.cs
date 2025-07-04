using BlazorProject.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public class LocationRepository : ILocationRepository
    {
            private readonly AppDbContext appDbContext;

            public LocationRepository(AppDbContext appDbContext)
            {
                this.appDbContext = appDbContext;
            }

            public async Task<Location> GetLocation(int locationId)
            {
                return await appDbContext.Location
                    .FirstOrDefaultAsync(d => d.LocationId == locationId);
            }

            public async Task<IEnumerable<Location>> GetLocations()
            {
                return await appDbContext.Location.ToListAsync();
            }
       


    }
}
