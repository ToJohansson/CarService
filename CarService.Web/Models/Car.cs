namespace CarService.Web.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string EngineType { get; set; }

    public List<ServiceItem> ServiceItems { get; set; } = new();
}
