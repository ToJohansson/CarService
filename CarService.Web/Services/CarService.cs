using CarService.Web.Models;

namespace CarService.Web.Services;

public class CarService

{
    List<Car> cars = new List<Car>();

    public CarService()
    {
        Car car = new Car { Id = 1234, Brand = "Citroen", Model = "C3 Picasso", Engine = "Petrol", Year = 2009 };

        car.ServiceItems = new List<ServiceItem>
{
    new ServiceItem
    {
        Id = 1,
        Name = "Engine Oil",
        Description = "Oil change with 5W-30 or 5W-40. Capacity ~4.3L.",
        KmInterval = 10000,
        TimeIntervalYears = 1,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 2,
        Name = "Air Filter",
        Description = "Check earlier if driving in dusty conditions.",
        KmInterval = 20000,
        TimeIntervalYears = null,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 3,
        Name = "Fuel Filter",
        Description = "Fuel filter in engine bay.",
        KmInterval = 60000,
        TimeIntervalYears = null,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 4,
        Name = "Fuel Tank Filter",
        Description = "Fuel filter in tank – less frequent replacement.",
        KmInterval = 100000,
        TimeIntervalYears = null,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 5,
        Name = "Timing Chain",
        Description = "No official interval. Check condition after 60,000 km.",
        KmInterval = 60000,
        TimeIntervalYears = null,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 6,
        Name = "Serpentine Belt",
        Description = "Drive/alternator belt replacement.",
        KmInterval = 60000,
        TimeIntervalYears = null,
        LastService = new DateTime(2024, 6, 1)
    },
    new ServiceItem
    {
        Id = 7,
        Name = "Engine Coolant",
        Description = "Coolant replacement. Capacity 6L.",
        KmInterval = 100000,
        TimeIntervalYears = 5,
        LastService = new DateTime(2024, 6, 1)
    }
};

        cars.Add(car);

    }
}
