using APBD8.Models;

namespace APBD8.Repositories;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetTripsAsync();
}