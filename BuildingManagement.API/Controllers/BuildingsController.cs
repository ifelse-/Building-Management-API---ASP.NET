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
        var result = _buildingService.GetAll();
        _logger.LogInformation("Getting all buildings.");
        return Ok(ApiResponseFactory.Success(result.Data, result.Message));
    }

    // GET: api/buildings/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _buildingService.GetById(id);
        
        if (!result.IsSuccess)
        {
            return NotFound(ApiResponseFactory.Fail<Building>(result.Message));
        }

        _logger.LogInformation("Getting building with ID {BuildingId}", id);
        return Ok(ApiResponseFactory.Success(result.Data, result.Message));

        //return result.IsSuccess
           // ? Ok(ApiResponse.Success(result.Data, result.Message))
           // : NotFound(ApiResponse.Fail(result.Message));
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

        _logger.LogInformation("Creating building {BuildingName}", dto.Name);

        var result = await _buildingService.Create(building);

        if (!result.IsSuccess)
        {
            return BadRequest(ApiResponseFactory.Fail<Building>(result.Message));
        }
        return Ok(ApiResponseFactory.Success(result.Data, result.Message));
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

        if (!result.IsSuccess)
        {
            return NotFound(ApiResponseFactory.Fail<Building>(result.Message));
        }

        _logger.LogInformation("Updating building {BuildingId}", id);
        return Ok(ApiResponseFactory.Success(result.Data, result.Message));
    }

    // Delete: api/buildings/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _buildingService.Delete(id);

        if (!result.IsSuccess)
        {
            return NotFound(ApiResponseFactory.Fail<Building>(result.Message));
        }
        _logger.LogInformation("Deleted building id {BuildingId}", id);
        return Ok(ApiResponseFactory.Success(result.Data, result.Message));
    }
}