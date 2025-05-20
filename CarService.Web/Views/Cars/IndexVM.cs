namespace CarService.Web.Views.Cars;

public class IndexVM
{
    public required CarVM[] Cars { get; set; }

    public class CarVM
    {
        public required int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
    }
}
