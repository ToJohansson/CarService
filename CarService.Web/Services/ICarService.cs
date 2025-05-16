using CarService.Web.Models;

namespace CarService.Web.Services;

public interface ICarService
{
    // Add new car
    bool AddCar(Car car);
    // List all cars
    Car[] GetAllCars();
    // Get car by ID
    Car? GetCarById(int id);

    // Delete Car By ID
    bool DeleteCarById(int id);

    // Service Items 
    bool AddServiceItem(int carId, ServiceItem item);
    bool RemoveServiceItem(int carId, int serviceItemId);
    ServiceItem? GetServiceItemById(int carId, int serviceItemId);
    bool UpdateLastServiceDate(int carId, int serviceItemId, DateTime newDate);
}
