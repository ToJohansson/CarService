using System.ComponentModel.DataAnnotations;

namespace CarService.Web.Models;

public class ServiceItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int? KmInterval { get; set; }


    public int? TimeIntervalMonths { get; set; }

    public DateTime LastService { get; set; }

    public int TripMeterWhenService { get; set; }

}
