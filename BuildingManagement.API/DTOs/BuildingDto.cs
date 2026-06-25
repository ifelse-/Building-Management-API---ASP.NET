namespace BuildingManagement.API.DTOs;

public class BuildingDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int NumberOfUnits { get; set; }
    }