using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BuildingManagement.API.Entities;
using BuildingManagement.API.Interfaces;
using BuildingManagement.API.DTOs;
using BuildingManagement.API.Models;

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
        return Ok(new ApiResponse<IEnumerable<Building>>
        {
            Success = true,
            Message = "Buildings retrieved successfully.",
            Data = buildings
        });
    }

    // GET: api/buildings/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var building = _buildingService.GetById(id);
        
        if (building == null)
        {
            return NotFound(new ApiResponse<Building>
            {
                Success = false,
                Message = "Building not found.",
                Data = null
            });
        }

        _logger.LogInformation("Getting building with ID {BuildingId}", id);

        return Ok(new ApiResponse<Building>
        {
            Success = true,
            Message = "Building retrieved successfully.",
            Data = building
        });
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

        // await _buildingService.Create(building);

        _logger.LogInformation("Creating building {BuildingName}", dto.Name);

        var createdBuilding = await _buildingService.Create(building);

        return Ok(new ApiResponse<Building>
        {
            Success = true,
            Message = "Building created successfully.",
            Data = createdBuilding
        });
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
        {
            return NotFound(new ApiResponse<Building>
            {
                Success = false,
                Message = "Building not found.",
                Data = null
            });
        }

        _logger.LogInformation("Updating building {BuildingId}", id);

        return Ok(new ApiResponse<Building>
        {
            Success = true,
            Message = "Building updated successfully.",
            Data = result
        });
    }

    // Delete: api/buildings/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _buildingService.Delete(id);

        if (!result)
        {
            return NotFound(new ApiResponse<Building>
            {
                Success = false,
                Message = "Building not found.",
                Data = null
            });
        }

        _logger.LogInformation("Deleted building id {BuildingId}", id);

        // return NoContent();

        return Ok(new ApiResponse<bool>
        {
            Success = true,
            Message = "Building deleted successfully.",
            Data = true
        });
    }
}