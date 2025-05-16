using CarService.Web.Models;

namespace CarService.Web.Services;

public interface ICarService
{
    // Add new car
    bool AddCar(Car car);
    // List all cars
    List<Car> GetAll();
    // Get car by ID
    Car GetById(int id);

    // Delete Car By ID
    bool DeleteById(int id);

    // Service Items 
    bool AddServiceItem(int carId, ServiceItem item);
    bool RemoveServiceItem(int carId, int serviceItemId);
    ServiceItem? GetServiceItemById(int carId, int serviceItemId);
    bool UpdateLastServiceDate(int carId, int serviceItemId, DateTime newDate);
}
