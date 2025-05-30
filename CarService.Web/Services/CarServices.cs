﻿using CarService.Web.Models;
using CarService.Web.Views.Cars;
using System.Reflection;
namespace CarService.Web.Services;

public class CarServices : ICarService

{
    List<Car> cars = new List<Car>();
    public CarServices()
    {
        Car car = new Car { Id = 1234, Brand = "Citroen", Model = "C3 Picasso", EngineType = "Petrol", Year = 2009, TripMeter = 15750 };

        car.ServiceItems = new List<ServiceItem>
{
    new ServiceItem
    {
        Id = 1,
        Name = "Engine Oil",
        Description = "Oil change with 5W-30 or 5W-40. Capacity ~4.3L.",
        KmInterval = 10000,
        TimeIntervalMonths = 12,
        LastService = new DateTime(2024, 6, 1),
        TripMeterWhenService = 15750
    },
    new ServiceItem
    {
        Id = 2,
        Name = "Air Filter",
        Description = "Check earlier if driving in dusty conditions.",
        KmInterval = 20000,
        TimeIntervalMonths = 24,
        LastService = new DateTime(2002, 6, 1),
                TripMeterWhenService = 12453

    },
    new ServiceItem
    {
        Id = 3,
        Name = "Fuel Filter",
        Description = "Fuel filter in engine bay.",
        KmInterval = 60000,
        TimeIntervalMonths = 12,
        LastService = new DateTime(2024, 6, 1),
        TripMeterWhenService = 10750

    },
    new ServiceItem
    {
        Id = 4,
        Name = "Fuel Tank Filter",
        Description = "Fuel filter in tank – less frequent replacement.",
        KmInterval = 100000,
        TimeIntervalMonths = 18,
        LastService = new DateTime(2025, 5, 1),
        TripMeterWhenService = 14750
    },
    new ServiceItem
    {
        Id = 5,
        Name = "Timing Chain",
        Description = "No official interval. Check condition after 60,000 km.",
        KmInterval = 60000,
        TimeIntervalMonths = 18,
        LastService = new DateTime(2024, 6, 1),
        TripMeterWhenService = 15750
    },
    new ServiceItem
    {
        Id = 6,
        Name = "Serpentine Belt",
        Description = "Drive/alternator belt replacement.",
        KmInterval = 60000,
        TimeIntervalMonths = 12,
        LastService = new DateTime(2025, 5, 1),
        TripMeterWhenService = 15750
    },
    new ServiceItem
    {
        Id = 7,
        Name = "Engine Coolant",
        Description = "Coolant replacement. Capacity 6L.",
        KmInterval = 100000,
        TimeIntervalMonths = 72,
        LastService = new DateTime(2024, 6, 1),
        TripMeterWhenService = 15750
    }
};
        cars.Add(car);
    }

    public bool AddCar(Car car)
    {
        int newId = 0;
        if (cars.Count == 0)
            newId = 1;
        else
            newId = cars.Max(car => car.Id) + 1;

        if ((GetCarById(newId) != null))
            return false;

        car.Id = newId;
        cars.Add(car);
        return true;
    }

    public bool UpdateCar(UpdateCarVM car)
    {
        var carToUpdate = GetCarById(car.CarId);
        if (carToUpdate == null)
            return false;

        carToUpdate.Brand = car.Brand;
        carToUpdate.Model = car.Model;
        carToUpdate.Year = car.Year;
        carToUpdate.TripMeter = car.TripMeter;
        carToUpdate.EngineType = car.EngineType;

        return true;
    }


    public bool DeleteCarById(int id)
    {
        var carToDelete = GetCarById(id);
        if (carToDelete == null)
            return false;

        cars.Remove(carToDelete);
        return true;
    }

    public Car[] GetAllCars() => cars.OrderBy(c => c.Brand).ToArray();


    public Car? GetCarById(int id)
    {
        return cars.SingleOrDefault(c => c.Id == id);
    }

    public ServiceItem? GetServiceItemById(int carId, int serviceItemId)
    {
        var car = GetCarById(carId);
        if (car == null)
            throw new Exception("could not find car");

        return car.ServiceItems.SingleOrDefault(s => s.Id == serviceItemId);
    }

    public bool AddServiceItem(int carId, ServiceItem item)
    {

        int newId = 0;
        var car = GetCarById(carId);
        if (car == null)
            return false;

        if (car.ServiceItems.Count == 0)
            newId = 1;
        else
            newId = car.ServiceItems.Max(s => s.Id) + 1;

        item.Id = newId;
        item.TripMeterWhenService = car.TripMeter;
        car.ServiceItems.Add(item);
        return true;
    }
    public bool UpdateServiceItem(UpdateServiceItemVM vm)
    {
        var model = GetCarById(vm.CarId);
        if (model == null)
            return false;


        var existing = model.ServiceItems.FirstOrDefault(x => x.Id == vm.Id);
        if (existing != null)
        {
            existing.Name = vm.Name;
            existing.Description = vm.Description;
            existing.KmInterval = vm.KmInterval;
            existing.TimeIntervalMonths = vm.TimeIntervalMonths;
            existing.TripMeterWhenService = vm.TripMeterWhenService;
            existing.LastService = vm.LastService;
        }

        return true;
    }


    public bool RemoveServiceItem(int carId, int serviceItemId)
    {
        var car = GetCarById(carId);
        if (car == null)
            return false;

        var item = car.ServiceItems.SingleOrDefault(s => s.Id == serviceItemId);
        if (item == null)
            return false;

        return car.ServiceItems.Remove(item);
    }


}

