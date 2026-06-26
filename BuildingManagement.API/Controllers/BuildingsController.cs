using Microsoft.AspNetCore.Mvc;
using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.DTOs;

namespace BuildingManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuildingsController : ControllerBase
{
    private readonly IBuildingService _buildingService;

    public BuildingsController(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    // GET: api/buildings
    [HttpGet]
    public IActionResult GetAll()
    {
        var buildings = _buildingService.GetAll();
        return Ok(buildings);
    }

    // GET: api/buildings/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var building = _buildingService.GetById(id);

        if (building == null)
            return NotFound();

        return Ok(building);
    }


    // POST: api/buildings
    [HttpPost]
    public async Task<IActionResult> Create(CreateBuildingDto dto)
        {
        var building = new Building
        {
            Name = dto.Name,
            Address = dto.Address,
            NumberOfUnits = dto.NumberOfUnits
        };

        await _buildingService.Create(building);

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