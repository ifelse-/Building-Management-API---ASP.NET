using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.DTOs;

namespace BuildingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;
    private readonly ILogger<BuildingsController> _logger;

    public BuildingsController(
        IBuildingService buildingService,
        ILogger<BuildingsController> logger)
    {
        _buildingService = buildingService;
        _logger = logger;
    }

    // GET: api/buildings
    [HttpGet]
    public IActionResult GetAll()
    {
        var buildings = _buildingService.GetAll();
        _logger.LogInformation("Getting all buildings.");
        return Ok(buildings);
    }

    // GET: api/buildings/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var building = _buildingService.GetById(id);
        
        if (building == null)
            return NotFound();

        _logger.LogInformation("Getting building with ID {BuildingId}", id);
        return Ok(building);
    }


    // POST: api/buildings
    [HttpPost]
    public async Task<IActionResult> Create(CreateBuildingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var building = new Building
        {
            Name = dto.Name,
            Address = dto.Address,
            NumberOfUnits = dto.NumberOfUnits
        };

        await _buildingService.Create(building);

        _logger.LogInformation("Creating building {BuildingName}", dto.Name);
        return Ok(building);
    }

    // Update: api/buildings/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateBuildingDto dto)
    {
        var updated = new Building
        {
            Name = dto.Name,
            Address = dto.Address,
            NumberOfUnits = dto.NumberOfUnits
        };

        var result = await _buildingService.Update(id, updated);

        if (result == null)
            return NotFound();

        _logger.LogInformation("Updating building {BuildingName}", id);
        return Ok(result);
    }

    // Delete: api/buildings/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _buildingService.Delete(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}