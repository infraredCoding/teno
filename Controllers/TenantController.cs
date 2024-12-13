using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tenants.Models;

namespace tenants.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantController : ControllerBase
{
    private readonly TenantContext _dbContext;

    public TenantController(TenantContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("properties")]
    public async Task<IEnumerable<Property>> GetProperties()
    {
        return await _dbContext.Properties.Include(p => p.Tenant).ToArrayAsync();
    }


    [HttpPost("properties")]
    public async Task<IActionResult> CreateProperty([FromBody] Property entity) {
        await _dbContext.Properties.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return Created();
    }

    [HttpPatch("properties/{id}")]
    public async Task<IActionResult> UpdateProperty([FromBody] Property entity, int id) {
        var property = await _dbContext.Properties.FindAsync(id);
        property.Name = entity.Name;
        property.Rent = entity.Rent;

        _dbContext.Properties.Update(property);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("properties/{id}")]
    public async Task<IActionResult> DeleteProperty(int id) {
        var property = await _dbContext.Properties.FindAsync(id);

        _dbContext.Properties.Remove(property);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }


    [HttpPost("properties/tenant/{id}")]
    public async Task<IActionResult> AddTenantToProperty([FromBody] Tenant tenantEntity, int id){
        var property = await _dbContext.Properties.Include(p => p.Tenant).FirstOrDefaultAsync(p => p.Id == id);
        if (property.Tenant != null){
            property.Tenant.Name = tenantEntity.Name;
            property.Tenant.FathersName = tenantEntity.FathersName;

            await _dbContext.SaveChangesAsync();
        }
        tenantEntity.PropertyId = id;
        _dbContext.Tenants.Add(tenantEntity);
        
        
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
