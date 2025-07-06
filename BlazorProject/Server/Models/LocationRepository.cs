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

        public async Task<LocationDto> GetLocationDto()
        {
            
        }

         public async Task<Location> AddLocation(Location location)

         {
            if (location != null)
            {
                appDbContext.Entry(location).State = EntityState.Unchanged;
            }

            var result = await appDbContext.Location.AddAsync(location);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
         }

        public async Task<Location> UpdateLocation(Location location)
        {
            var result = await appDbContext.Location
                .FirstOrDefaultAsync(e => e.LocationId == location.LocationId);

            if (result != null)
            {
                result.LocationName = location.LocationName;
                result.LocationDescrption = location.LocationDescrption;

                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
        public async Task DeleteLocation(int locationId)
        {
            var result = await appDbContext.Location
                .FirstOrDefaultAsync(e => e.LocationId == locationId);

            if (result != null)
            {
                appDbContext.Location.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
