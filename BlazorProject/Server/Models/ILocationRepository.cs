using BlazorProject.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProject.Server.Models
{
    public interface ILocationRepository
    {

        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocation(int locationId);
        Task<Location> AddLocation(Location location);
        Task<Location> UpdateLocation(Location location);
        Task DeleteLocation(int locationId);
    }
}
