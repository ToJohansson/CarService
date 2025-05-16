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


}
