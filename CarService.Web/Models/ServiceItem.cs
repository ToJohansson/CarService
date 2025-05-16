namespace CarService.Web.Models;

public class ServiceItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } // update ? 

    public int? KmInterval { get; set; }
    public int? TimeIntervalYears { get; set; }
    public DateTime LastService { get; set; } // update

}
