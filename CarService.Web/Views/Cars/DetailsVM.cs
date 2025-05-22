using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Views.Cars;

public class DetailsVM
{
    public required int Id { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required int Year { get; set; }
    public required string EngineType { get; set; }
    public required int TripMeter { get; set; }
    public required ServiceItemsVM[] serviceItemsVM { get; set; }

    public class ServiceItemsVM
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required int? KmInterval { get; set; }

        public required int? TimeIntervalMonths { get; set; }

        public required int TripMeterWhenService { get; set; }
        public required DateTime LastService { get; set; }
        public ServiceStatusVM Status { get; set; }

    }
}
public enum ServiceStatusVM
{
    Ok,
    Due,
    Overdue
}
