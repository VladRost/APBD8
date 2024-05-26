using APBD8.Context;
using APBD8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD8.Controllers;
[Route("api/[controller]")]
public class tripsController:ControllerBase
{
    private readonly MasterContext _dbContext;

    public tripsController(MasterContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var countTrips = await _dbContext.Trips.CountAsync();
        var trips = await _dbContext.Trips
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new {
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                Countries = t.IdCountries.Select(c => new { c.Name }),
                Clients = t.ClientTrips.Select(ct => new { ct.IdClientNavigation.FirstName,ct.IdClientNavigation.LastName})
            })
            .ToListAsync();

        var result = new
        {
            pageNum = page,
            pageSize,
            allPages = (int)Math.Ceiling((double)countTrips / pageSize),
            trips
        };

        return Ok(result);
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, [FromBody] ClientAddTrip clientAddTrip)
    {

        var existingClient = await _dbContext.Clients.FirstAsync(c => c.Pesel == clientAddTrip.Pesel);
        if (existingClient != null)
        {
            return BadRequest(new { message = "Client already exists" });
        }

        var checkTrip = await _dbContext.ClientTrips.AnyAsync(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == idTrip);
        if (checkTrip)
        {
            return BadRequest(new { message = "Client is already registered for the trip" });
        }
        
        var trip = await _dbContext.Trips.FindAsync(idTrip);
        if (trip == null || trip.DateFrom < DateTime.UtcNow)
        {
            return BadRequest(new { message = "Trip does not exist or has already started" });
        }
        
        var client = new Client
        {
            FirstName = clientAddTrip.FirstName,
            LastName = clientAddTrip.LastName,
            Email = clientAddTrip.Email,
            Telephone = clientAddTrip.Telephone,
            Pesel = clientAddTrip.Pesel
        };

        _dbContext.Clients.Add(client);
        await _dbContext.SaveChangesAsync();

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            PaymentDate = clientAddTrip.PaymentDate,
            RegisteredAt = DateTime.UtcNow
        };

        _dbContext.ClientTrips.Add(clientTrip);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}