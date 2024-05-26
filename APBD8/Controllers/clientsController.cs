using APBD8.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD8.Controllers;
[Route("api/[controller]")]
public class clientsController:ControllerBase
{
    private readonly MasterContext _dbContext;

    public clientsController(MasterContext context)
    {
        _dbContext = context;
    }
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _dbContext.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client.ClientTrips.Any())
        {
            return BadRequest(new { message = "Client cannot be deleted" });
        }

        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
    
}