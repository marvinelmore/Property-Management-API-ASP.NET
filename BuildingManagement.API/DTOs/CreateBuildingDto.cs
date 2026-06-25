namespace BuildingManagement.API.DTOs;

public class CreateBuildingDto
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int NumberOfUnits { get; set; }
    }