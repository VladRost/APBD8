// using APBD8.Context;
// using APBD8.Models;
//
// namespace APBD8.Repositories;
//
// public class TripRepository:ITripRepository
// {
//     public async Task<IEnumerable<Trip>> GetTripsAsync()
//     {
//         var dbContext = new MasterContext();
//         var trips = await dbContext.Trips.OrderByDescending(d => d.DateFrom)
//     }
// }